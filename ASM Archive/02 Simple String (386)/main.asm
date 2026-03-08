TITLE		03 Print a String
SUBTITLE	Based on Duntemann p.284

.MODEL	TINY
.286
ASSUME DS:DATA, CS:CODE, SS:STACK

stack SEGMENT 'STACK'
stack ENDS

data SEGMENT
                  ; Combined 0-based X,Y of 80 x 25 screen LR corner:
LRXY      DW      184FH  ; 18H = 24D; 4FH = 79D

TextPos   DW      0   ; Memory variable to store text screen coordinates

EatMsg1   DB      "Eat at Joe's...",'$'
EatMsg2   DB      "...ten million flies can't ALL be wrong!",'$'
CRLF      DB      0DH,0AH,'$'
Space     DB      " ",'$'

data ENDS

code SEGMENT
.startup
call ClrScr        ; Clear the full display

    ; Make sure you understand the difference between
    ;   MOV DX,Identifier and MOV DX,[Identifier] !!!

    mov  [TextPos],0914H   ; 0914H = X @ 20, Y @ 9

    mov  DX,[TextPos]  ; TextPos contains X,Y position values
    call GotoXY        ; Position cursor
    mov  DX,OFFSET EatMsg1    ; Load offset of EatMsg1 string into DX
    call Write         ;   and display it

    mov  DX,[TextPos]  ; Re-use text position variable
    mov  DH,10         ; Put new Y value into DH but re-use X
    call GotoXY        ; Position cursor
    mov  DX,OFFSET EatMsg2    ; Load offset of EatMsg2 string into DX
    call Write         ;   and display it

    mov  DX,1701H      ; Move cursor to bottom left corner of screen
    call GotoXY        ;   so that 'Press enter...' msg is out of the way.

    mov  ax,4C00H      ; This function exits the program
    int  21H           ;   and returns control to DOS.

;----------------------------------------|
;           PROCEDURE SECTION            |
;----------------------------------------|

;---------------------------------------------------------------
;   GOTOXY    --  Positions the hardware cursor to X,Y
;   Last update 7/31/99
;
;   1 entry point:
;
;   GotoXY:
;      Caller must pass:
;      DL: X value     These are both 0-based; i.e., they
;      DH: Y value       assume a screen 24 by 79, not 25 by 80
;      Action:  Moves the hardware cursor to the X,Y position
;               loaded into DL and H.
;---------------------------------------------------------------
GotoXY:
    mov AH,02H        ; Select VIDEO service 2: Position cursor
    mov BH,0          ; Stay with display page 0
    int 10H           ; Call VIDEO
    ret               ; Return to the caller


;---------------------------------------------------------------
;   CLRSCR    --  Clears or scrolls screens or windows
;   Last update 3/5/89
;
;   4 entry points:
;
;   ClrScr:
;      No values expected from caller
;      Action:  Clears the entire screen to blanks with 07H as
;               the display attribute
;
;   ClrWin:
;      Caller must pass:
;      CH: Y coordinate, upper left corner of window
;      CL: X coordinate, upper left corner of window
;      DH: Y coordinate, lower right corner of window
;      DL: X coordinate, lower right corner of window
;      Action:  Clears the window specified by the caller to
;               blanks with 07H as the display attribute
;
;   ScrlWin:
;      Caller must pass:
;      CH: Y coordinate, upper left corner of window
;      CL: X coordinate, upper left corner of window
;      DH: Y coordinate, lower right corner of window
;      DL: X coordinate, lower right corner of window
;      AL: number of lines to scroll window by (0 clears it)
;      Action:  Scrolls the window specified by the caller by
;               the number of lines passed in AL.  The blank
;               lines inserted at screen bottom are cleared
;               to blanks with 07H as the display attribute
;
;   VIDEO6:
;      Caller must pass:
;      CH: Y coordinate, upper left corner of window
;      CL: X coordinate, upper left corner of window
;      DH: Y coordinate, lower right corner of window
;      DL: X coordinate, lower right corner of window
;      AL: number of lines to scroll window by (0 clears it)
;      BH: display attribute for blanked lines (07H is "normal")
;      Action:  Generic access to BIOS VIDEO service 6.  Caller
;               must pass ALL register parameters as shown above
;---------------------------------------------------------------

ClrScr:
    mov CX,0          ; Upper left corner of full screen
    mov DX,LRXY       ; Load lower-right XY coordinates into DX
ClrWin:
    mov AL,0          ; 0 specifies clear entire region
ScrlWin:
    mov BH,07H        ; Specify "normal" attribute for blanked line(s)
VIDEO6:
    mov AH,06H        ; Select VIDEO service 6: Initialize/Scroll
    int 10H           ; Call VIDEO
    ret               ; Return to the caller


;---------------------------------------------------------------
;   WRITE    --  Displays information to the screen via DOS
;                service 9: Print String
;   Last update 7/31/99
;
;   1 entry point:
;
;   Write:
;      Caller must pass:
;      DS: The segment of the string to be displayed
;      DX: The offset of the string to be displayed
;          String must be terminated by "$"
;      Action:  Displays the string at DS:DX up to the "$" marker
;---------------------------------------------------------------

Write:
    mov AH,09H        ; Select DOS service 9: Print String
    int 21H           ; Call DOS
    ret               ; Return to the caller


;---------------------------------------------------------------
;   WRITELN  --  Displays information to the screen via DOS
;                service 9 and issues a newline
;   Last update 7/31/99
;
;   1 entry point:
;
;   Writeln:
;      Caller must pass:
;      DS: The segment of the string to be displayed
;      DX: The offset of the string to be displayed
;          String must be terminated by "$"
;      Action:  Displays the string at DS:DX up to the "$" marker
;               marker, then issues a newline.  Hardware cursor
;               will move to the left margin of the following
;               line.  If the display is to the bottom screen
;               line, the screen will scroll.
;      Calls: Write
;---------------------------------------------------------------

Writeln:
    call Write        ; Display the string proper through Write
    mov DX,OFFSET CRLF       ; Load offset of newline string to DX
    call Write        ; Display the newline string through Write
    ret               ; Return to the caller

END