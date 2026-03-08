using ComputerControls;

namespace Motherboard
{
    public class CPU
    {
        string output;
        public string Output { get { return output; } }

        Register ip = new Register();
        Register ax = new Register();

        Memory memory = new Memory();
        public Memory Memory { get { return memory; } }

        Disk disk = new Disk();

        DataBus dataBus = new DataBus();
        SystemBus systemBus = new SystemBus();

        public CPU()
        {
            ip.Value = 115;

            memory.LoadFromDisk(disk, 0, 8);
        }

        public void Mov(byte address, byte value)
        {
            //Put value on databus
            DataBus.Set(value);

            //Put address on address bus
            AddressBus.Data = address;

            //assert write
            SystemBus.Write = true;
        }

        public void Mov(string register, byte value)
        {
            if (register == "ax") ax.Value = value;
        }

        public void Put()
        {
            output = ax.ToString();
        }
    }
}
