.model small
.stack
.data

 message   db "Assembly", "$"

 .code 
 
 _main   proc 


   mov dx,OFFSET message          
   mov   ah,09
   lea   dx,message
   int   21h

   mov   ax,4c00h
   int   21h
_main   endp
end _main