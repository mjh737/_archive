using System;
using System.Collections.Generic;
using System.Text;
using ComputerControls;

namespace Motherboard
{
    public class Memory
    {
        Clock clock = new Clock();
        byte[] bytes;

        public byte this[int index]
        {
            get { return bytes[index]; }
            set { bytes[index] = value; }
        }

        public Memory ()
	    {
            bytes = new byte[8];

            clock.Tick += new TickDelegate(clock_Tick);
	    }

        void clock_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (SystemBus.Write)
            {
                this[AddressBus.Data] = DataBus.Get();
                SystemBus.Write = false;
            }
        }

        public void LoadFromDisk(Disk disk, int startAddress, int numBytes)
        {
            for (int i = 0; i < numBytes; i++)
            {
                this[i] = disk[startAddress + i];
            }
        }
    }
}
