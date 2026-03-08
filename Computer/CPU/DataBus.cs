using System;
using System.Collections.Generic;
using System.Text;

namespace Motherboard
{
    public class DataBus
    {
        static byte myByte;

        static bool d0;
        static bool d1;
        static bool d2;
        static bool d3;
        static bool d4;
        static bool d5;
        static bool d6;
        static bool d7;

        public static bool D0 { get { return d0; } set { d0 = value; } }
        public static bool D1 { get { return d1; } set { d1 = value; } }
        public static bool D2 { get { return d2; } set { d2 = value; } }
        public static bool D3 { get { return d3; } set { d3 = value; } }
        public static bool D4 { get { return d4; } set { d4 = value; } }
        public static bool D5 { get { return d5; } set { d5 = value; } }
        public static bool D6 { get { return d6; } set { d6 = value; } }
        public static bool D7 { get { return d7; } set { d7 = value; } }

        

        public static void Set(byte b)
        {
            D0 = (b & 1) == 1;
            D1 = (b & 2) == 2;
            D2 = (b & 4) == 4;
            D3 = (b & 8) == 8;
            D4 = (b & 16) == 16;
            D5 = (b & 32) == 32;
            D6 = (b & 64) == 64;
            D7 = (b & 128) == 128;

            myByte = b;
        }

        public static byte Get()
        {
            return myByte;
        }
    }
}
