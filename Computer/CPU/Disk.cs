using System;
using System.Collections.Generic;
using System.Text;
using ComputerControls;

namespace Motherboard
{
    public class Disk
    {
        byte[] bytes;

        public byte this[int index]
        {
            get { return bytes[index]; }
            set { bytes[index] = value; }
        }

        public Disk()
        {
            bytes = new byte[8];

            bytes[0] = 10;
            bytes[1] = 189;
            bytes[2] = 1;
            bytes[3] = 123;
            bytes[4] = 0;
            bytes[5] = 210;
            bytes[6] = 255;
            bytes[7] = 108;
        }
    }
}
