; loader.asm - stage 2 loader
; Loaded by boot sector at 0x0000:0x1000
; - Starts in 16-bit real mode
; - Switches to 32-bit protected mode
; - Sets up PAE paging
; - Enters 64-bit long mode
; - Confirms long mode and prints "LM OK" or "FAIL"

BITS 16
ORG 0x1000                 ; This is where boot.asm jumps to

start_loader:
    ; -------------------------------
    ; Basic 16-bit setup
    ; -------------------------------
    cli                     ; Just to be safe
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x9000          ; Simple stack

    ; -------------------------------
    ; Load GDT and enter protected mode
    ; -------------------------------
    lgdt [gdt_descriptor]   ; Load our GDT

    mov eax, cr0
    or eax, 1               ; Set PE bit
    mov cr0, eax

    jmp 0x08:pm_entry       ; Far jump to 32-bit code segment


; ------------------------------------------------------
; 32-bit protected mode
; ------------------------------------------------------
[BITS 32]
pm_entry:
    ; Set up data segments
    mov ax, 0x10            ; Data segment selector
    mov ds, ax
    mov es, ax
    mov fs, ax
    mov gs, ax
    mov ss, ax

    mov esp, 0x90000        ; 32-bit stack

    ; --------------------------------------------------
    ; Clear paging structures (PML4, PDPT, PD)
    ; --------------------------------------------------
    mov edi, pml4_table     ; Start of paging structures
    mov ecx, (4096 * 3) / 4 ; 3 pages * 4KB / 4 bytes per dword
    xor eax, eax
clear_page_tables:
    mov [edi], eax
    add edi, 4
    loop clear_page_tables

    ; --------------------------------------------------
    ; PML4[0] -> PDPT
    ; --------------------------------------------------
    mov eax, pdpt_table
    or eax, 0x003           ; Present + Writeable
    mov [pml4_table], eax

    ; --------------------------------------------------
    ; PDPT[0] -> PD
    ; --------------------------------------------------
    mov eax, pd_table
    or eax, 0x003           ; Present + Writeable
    mov [pdpt_table], eax

    ; --------------------------------------------------
    ; PD[0] = 2MB page at 0x00000000
    ; --------------------------------------------------
    mov eax, 0x00000000     ; Physical base
    or eax, 0x083           ; Present + Writeable + Page Size (2MB)
    mov [pd_table], eax

    ; --------------------------------------------------
    ; Load CR3 with PML4 base, enable PAE
    ; --------------------------------------------------
    mov eax, pml4_table
    mov cr3, eax

    mov eax, cr4
    or eax, 1 << 5          ; PAE bit
    mov cr4, eax

    ; --------------------------------------------------
    ; Enable long mode via EFER MSR
    ; --------------------------------------------------
    mov ecx, 0xC0000080     ; EFER
    rdmsr                   ; EDX:EAX = EFER
    or eax, 1 << 8          ; LME bit
    wrmsr

    ; --------------------------------------------------
    ; Enable paging (CR0.PG)
    ; --------------------------------------------------
    mov eax, cr0
    or eax, 1 << 31         ; PG bit
    mov cr0, eax

    ; Now: PE=1, PAE=1, LME=1, PG=1 → long mode active
    ; Still executing in 32-bit compat mode until we jump
    ; to a 64-bit code segment.

    ; --------------------------------------------------
    ; Far jump to 64-bit code segment (selector 0x18)
    ; --------------------------------------------------
    jmp 0x18:long_mode_entry


; ------------------------------------------------------
; 64-bit long mode
; ------------------------------------------------------
[BITS 64]
long_mode_entry:
    ; Load data segments (required even in long mode)
    mov ax, 0x10
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov fs, ax
    mov gs, ax

    mov rsp, 0x80000        ; 64-bit stack

    ; --------------------------------------------------
    ; LONG MODE CONFIRMATION TESTS
    ; --------------------------------------------------

    ; Test 1: CS must be 0x18
    mov ax, cs
    cmp ax, 0x18
    jne lm_fail

    ; Test 2: EFER.LMA must be set
    mov ecx, 0xC0000080     ; EFER MSR
    rdmsr                   ; EDX:EAX = EFER
    bt eax, 10              ; Test LMA bit
    jnc lm_fail             ; If not set, fail

    ; Test 3: 64-bit-only instruction: push rax
    push rax
    pop rax
	
lm_ok:
    ; --------------------------------------------------
    ; Print "64-BIT MODE ACTIVE" to VGA text memory
    ; --------------------------------------------------

    mov rdi, 0xB8000         ; VGA text buffer base
    mov rsi, msg64           ; Pointer to message string
    mov ah, 0x1F             ; Attribute: bright white on blue

.print_loop:
    lodsb                    ; Load next character into AL
    cmp al, 0
    je lm_hang               ; End of string → stop
    mov [rdi], al            ; Write character
    mov [rdi+1], ah          ; Write attribute
    add rdi, 2               ; Advance to next cell
    jmp .print_loop

lm_fail:
    ; Write "FAIL" to VGA text memory
    mov rax, 0x1F204C4941461F46   ; Encodes "FAIL"
    mov qword [0xB8000], rax

lm_hang:
    hlt
    jmp lm_hang


; ------------------------------------------------------
; GDT with 32-bit and 64-bit code segments
; ------------------------------------------------------
[BITS 32]

gdt_start:
gdt_null:       dq 0                ; Null descriptor

; 0x08: 32-bit code segment
gdt_code32:
    dw 0xFFFF       ; Limit low
    dw 0x0000       ; Base low
    db 0x00         ; Base middle
    db 10011010b    ; Access: present, ring 0, executable, readable
    db 11001111b    ; Flags: 4K, 32-bit, limit high
    db 0x00         ; Base high

; 0x10: Data segment
gdt_data:
    dw 0xFFFF
    dw 0x0000
    db 0x00
    db 10010010b    ; Access: present, ring 0, writable
    db 11001111b    ; Flags: 4K, 32-bit, limit high
    db 0x00

; 0x18: 64-bit code segment
gdt_code64:
    dw 0x0000       ; Limit low (ignored)
    dw 0x0000       ; Base low
    db 0x00         ; Base middle
    db 10011010b    ; Access: present, ring 0, executable, readable
    db 00100000b    ; Flags: L=1 (64-bit), D=0, G=0
    db 0x00         ; Base high

gdt_end:

gdt_descriptor:
    dw gdt_end - gdt_start - 1      ; Size of GDT - 1
    dd gdt_start                    ; Linear/physical address of GDT

msg64: db "64-BIT MODE ACTIVE", 0

; ------------------------------------------------------
; Paging structures (4KB aligned)
; ------------------------------------------------------
align 4096
pml4_table:
    times 512 dq 0

align 4096
pdpt_table:
    times 512 dq 0

align 4096
pd_table:
    times 512 dq 0