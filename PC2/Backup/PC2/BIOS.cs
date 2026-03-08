using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC2
{
    public class BIOS
    {
        static byte[] b = new byte[] {

            7, //   00 int
            254, // 01 Clear the display
            7, //   02 int
            1, //   03 1 (char)
            77, //  04 M
            97, //  05 a
            116, // 06 t
            116, // 07 t
            32, //  08
            79, //  09 O
            83, //  10 S
            13, //  11 CR
            10, //  12 LF
            13, //  13 CR
            10, //  14 LF
            0, //   15 Terminator

            //Test Power
            7, //   16 int
            250, // 17 1 (char)
            103, // 18 cmp ax, constant
            255, // 19 255
            8, //   20 je if power on to
            24, //  21 this
            7, //   22 int
            255, // 23 reset
            7, //   24 int
            1, //   25 1 (char)
            80, //  26 P
            111, // 27 o
            119, // 28 w
            101, // 29 e
            114, // 30 r
            32, //  31
            79, //  32 O
            75, //  33 K
            13, //  34 CR
            10, //  35 LF
            13, //  36 CR
            10, //  37 LF
            0, //   38 Terminator

            //Test Memory
            7, //   39 int
            251, // 40 251 (get size of ram)
            135, // subtract const from ax
            1, //   42 1
            208,//  43 mov cx, ax
            7, //   44 int 252 = memory test
            252, // 45 252
            127, // 46 cmp bx, 255
            255, // 47 255
            104, // 48 jne if memory bad
            61,  // 49 to 61 if memory bad

            194, // 50 mov ax, cx  Put counter in ax
            7,   // 51 int         And display it with int 0
            0, //   52 0 shows contents of ax

            119, // 53 cmp cx, 0
            0, //   54 1
            8, //   55 if equal jump
            81, //  56 to next section at 81
            151, // 57 dec cx
            1,   // 58
            14, //  59 jump to int 252 above
            44, //  60 location of memory test int 252

            // MEMORY BAD
            7, //   61 int
            1, //   62 1 (char)
            77, //  63 M
            101, // 64 e
            109, // 65 m
            111, // 66 o
            114, // 67 r
            121, // 68 y
            32, //  69 
            69, //  70 E
            114, // 71 r
            114, // 72 r
            111, // 73 o
            114, // 74 r
            13, //  75 CR
            10, //  76 LF
            13, //  77 CR
            10, //  78 LF
            0, //   79 Terminator
            3, //   80 break
            // Memory ok so continue
            3, //   81 Continue from here
            };

        public static byte[] Load()
        {
            return b;
        }
    }
}
