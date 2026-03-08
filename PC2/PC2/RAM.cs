using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC2
{
    public class RAM
    {
        const byte RAM_SIZE_BYTES = 255; // 16k of RAM

        public byte Size { get { return RAM_SIZE_BYTES; } }

        byte[] data = new byte[RAM_SIZE_BYTES];

        public byte this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        public bool TestByte(byte b)
        {
            if (data[b] != 255) return true;

            return false;
        }
    }
}
