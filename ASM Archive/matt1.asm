.model small
.stack
.data

 message   db "Assembly", "$"

 .code 
 
 _main   proc 

MOV AH,02	; Function to output a char
MOV DL,"!"    	; Character to output
INT 21h		; Call the interrupt to output "!"
MOV AH,04Ch	; Select exit function
MOV AL,00	; Return 0
INT 21h		; Call the interrupt to exit

_main   endp
end _main