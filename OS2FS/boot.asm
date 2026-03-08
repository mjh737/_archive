; boot.asm - 16-bit BIOS boot sector (stage 1)
; - Prints a message
; - Loads stage-2 loader from disk into memory at 0x1000
; - Jumps to it

BITS 16
ORG 0x7C00                 ; BIOS loads us here

start:
    ; -------------------------------
    ; Set up segment registers
    ; -------------------------------
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00          ; Simple stack just below us

    mov [BOOT_DRIVE], dl    ; Save BIOS boot drive now that DS = 0

    ; -------------------------------
    ; Print a small banner
    ; -------------------------------
    mov si, msg
    mov ah, 0x0E            ; BIOS teletype

.print_loop:
    lodsb                   ; AL = [DS:SI], SI++
    cmp al, 0
    je .after_print
    int 0x10
    jmp .print_loop

.after_print:

    ; -------------------------------
    ; Load stage-2 loader from disk
    ; -------------------------------
    ; Assumptions:
    ; - Boot drive in BOOT_DRIVE
    ; - Loader starts at CHS: head 0, track 0, sector 2
    ; - We load 32 sectors into ES:BX = 0x0000:0x1000
    ; -------------------------------

    xor ax, ax              ; AX = 0
    mov es, ax              ; ES = 0x0000
    mov bx, 0x1000          ; Load address offset

    mov dh, 0               ; Head = 0
    mov ch, 0               ; Track = 0
    mov cl, 2               ; Sector = 2 (sector 1 is the boot sector)
    mov al, 32              ; Number of sectors to read (16 KB loader)
    mov ah, 0x02            ; BIOS function: read sectors
    mov dl, [BOOT_DRIVE]    ; Restore boot drive

    int 0x13                ; BIOS disk read
    jc disk_error           ; If carry set, error

    ; -------------------------------
    ; Jump to loaded stage-2 loader
    ; -------------------------------
    jmp 0x0000:0x1000       ; Far jump to ES:BX (0x0000:0x1000)

disk_error:
    ; Simple error message loop
    mov si, disk_err_msg
    mov ah, 0x0E
.err_loop:
    lodsb
    cmp al, 0
    je .halt
    int 0x10
    jmp .err_loop

.halt:
    cli
    hlt
    jmp .halt

; -------------------------------
; Data
; -------------------------------
msg:           db "Booting stage 2...", 0
disk_err_msg:  db "Disk read error!", 0
BOOT_DRIVE:    db 0

; -------------------------------
; Boot signature
; -------------------------------
times 510 - ($ - $$) db 0
dw 0xAA55