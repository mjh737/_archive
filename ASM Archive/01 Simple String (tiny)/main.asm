TITLE		01 Print a String
SUBTITLE	Based on Duntemann p.229

.MODEL	TINY
.386
option segment:use16

.data
message db "Matt is so cool!",13,10,"$"

.code
.startup
	mov dx, OFFSET message
	mov ah, 9h
	int 21h

	mov ax, 04C00h
	int 21h

END