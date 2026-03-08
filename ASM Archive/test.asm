.386                
.model flat, stdcall

option casemap :none
 
include \masm32\include\windows.inc
include \masm32\include\user32.inc
include \masm32\include\kernel32.inc
includelib \masm32\lib\user32.lib
includelib \masm32\lib\kernel32.lib

.data
atitle  db "Window Title",0
text       db "Window Content",0

.code

start:

mov ecx, 3

loop1:

push MB_YESNOCANCEL
push offset atitle
push offset text
push NULL
call MessageBox
sub ecx, 1

cmp ecx, 0
jne loop1

push 0
call ExitProcess

end start
