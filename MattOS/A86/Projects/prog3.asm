
start:
  mov ah,08
  int 21h
  mov bl,al

cmp bl, "g"
jz	output
jmp exit

  mov ah,01
  int 21h
output:
  mov dl,"("
  mov ah,02
  int 21h
  mov dl,bl
  int 21h
  mov dl,")"
  int 21h
exit:
  mov ah,4ch
  mov al,00
  int 21h
 
