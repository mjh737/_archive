jmp start

;--------------------------

msg 	db 	"Thank you for pressing $"
bye 	db 	"Goodbye!$"
crlf	db	10,13,"$"

;--------------------------



start:
  	mov ah,08
  	int 21h
  	mov bl,al

	mov ah, 09
	mov dx, offset msg
	int 21h

  	mov dl,bl
  	mov ah,02
  	int 21h

	mov ah, 09
	mov dx,offset crlf
  	int 21h

	mov ah, 09
	mov dx,offset bye
  	int 21h

	mov ah,4ch
	mov al,00
	int 21h
 
