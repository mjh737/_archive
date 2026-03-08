jmp start

;--------------------------

msg 	db 	"Thank you for pressing $"
bye 	db 	"Goodbye!$"
crlf	db	10,13,"$"

;--------------------------



start:
  	mov dl,"W"
  	mov ah,02
  	int 21h

	mov ah,4ch
	mov al,00
	int 21h
 
