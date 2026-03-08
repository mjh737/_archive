using System;
using System.Collections.Generic;
using System.Text;

namespace Motherboard
{
    class SystemBus
    {
        static bool read;
        static public bool Read { get { return read; } set { read = value; } }

        static bool write;
        public static bool Write { get { return write; } set { write = value; } }

        public SystemBus()
        {
            read = false;
            write = false;
        }

    }
}
