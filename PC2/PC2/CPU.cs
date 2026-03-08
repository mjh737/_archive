using System.Windows.Forms;

namespace PC2
{
    public partial class CPU : Form
    {
        byte AX = new byte();
        byte BX = new byte();
        byte CX = new byte();
        byte DX = new byte();
        byte IP = new byte();

        byte dataBus = new byte();
        byte systemBus = new byte(); // z,c,o,p

        RAM memory = new RAM();

        public CPU()
        {
            InitializeComponent();
            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            AXTextBox.Text = AX.ToString();
            BXTextBox.Text = BX.ToString();
            CXTextBox.Text = CX.ToString();
            DXTextBox.Text = DX.ToString();
            IPTextBox.Text = IP.ToString();

            DataBusTextBox.Text = dataBus.ToString();
            DisplayTextBox.Text = VDU.GetScreen();
        }

        private void Fetch()
        {
            dataBus = memory[IP];
            IP++;
        }

        private bool Decode()
        {
            if (dataBus == Converter.ConvertBitsToByte("00000011")) return false;
            if ((dataBus & Converter.ConvertBitsToByte("11100000")) == 0) SpecialOps();
            if ((dataBus & Converter.ConvertBitsToByte("11100000")) == Converter.ConvertBitsToByte("11000000")) MoveRegMRC();
            if ((dataBus & Converter.ConvertBitsToByte("11100000")) == Converter.ConvertBitsToByte("00011000")) Compare();
            if ((dataBus & Converter.ConvertBitsToByte("11100000")) == Converter.ConvertBitsToByte("10000000")) Subtract();

            return true;
        }

        private void Subtract()
        {
            // sub ax,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00000000"))
            {
                // sub ax, const
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000111"))
                {
                    Fetch();
                    AX -= dataBus;
                }
            }

            // sub bx,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00001000"))
            {
            }

            // sub cx,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00010000"))
            {
                // sub cx, const
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000111"))
                {
                    Fetch();
                    CX -= dataBus;
                }
            }

            // sub dx,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00011000"))
            {
            }

        }

        private void MoveRegMRC()
        {
                            // mov ax,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00000000"))
            {
                // mov ax, const
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000111"))
                {
                    AX = memory[dataBus & Converter.ConvertBitsToByte("00000111")];
                    Fetch();
                    AX += dataBus;
                }

                // mov ax, cx
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000010"))
                {
                    AX = CX;
                }

            }

            // mov bx,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00001000"))
            {
            }

            // mov cx,
            if ((dataBus & Converter.ConvertBitsToByte("00010000")) == Converter.ConvertBitsToByte("00010000"))
            {
                // mov cx, ax
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000000"))
                {
                    CX = AX;
                }
            }

            //mov dx,
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00011000"))
            {
            }
        }

        private void Compare()
        {
            //ax
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == 0)
            {
                //Constant
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000111"))
                {
                    Fetch(); // Fetch constant from stack
                    systemBus = 0; //Need to fix this
                    if (AX - dataBus == 0) systemBus |= 128;
                }
            }

            //bx
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00001000"))
            {
                //Constant
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000111"))
                {
                    Fetch(); // Fetch constant from stack
                    systemBus = 0; //Need to fix this
                    if (BX - dataBus == 0) systemBus |= 128;
                }
            }

            //cx
            if ((dataBus & Converter.ConvertBitsToByte("00011000")) == Converter.ConvertBitsToByte("00010000"))
            {
                //Constant
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000111"))
                {
                    Fetch(); // Fetch constant from stack
                    systemBus = 0; //Need to fix this
                    if (CX - dataBus == 0) systemBus |= 128;
                }
            }
        }

        private void SpecialOps()
        {
            //jmp
            if ((dataBus & Converter.ConvertBitsToByte("11111000")) == Converter.ConvertBitsToByte("00001000"))
            {
                //jmp
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000110"))
                {
                    Fetch();
                    IP = dataBus;
                }

                // je
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == 0)
                {
                    // If equal
                    if ((systemBus & 128) == 128)
                    {
                        Fetch();
                        IP = dataBus;
                    }
                    else //Skip the je target address
                        IP++;
                }

                // jne
                if ((dataBus & Converter.ConvertBitsToByte("00000111")) == Converter.ConvertBitsToByte("00000001"))
                {
                    //if not equal
                    if ((systemBus & 128) != 128)
                    {
                        Fetch();
                        IP = dataBus;
                    }
                    else //Skip the jne target address
                        IP++;
                }
            }
            //int
            if (dataBus == Converter.ConvertBitsToByte("00000111"))
            {
                Fetch();

                //int 0 prints content of AX to screen
                if (dataBus == 0)
                {
                    VDU.Print(AX.ToString());
                }

                //int 1 prints following bytes until a 255 is reached
                if (dataBus == 1)
                {
                    do
                    {
                        Fetch();
                        char c = Converter.ConvertByteToChar(dataBus);
                        if (c == '\0') break;
                        VDU.Print(c.ToString());
                    } while (true);
                }

                //int 250 tests the PSU and sets ax to 255 if ok
                if (dataBus == 250)
                {
                    AX = 0;
                    if (PSU.Test() == PsuStatus.POWER_GOOD) AX = 255;
                }

                //int 251 gets size of ram and puts it in ax
                if (dataBus == 251)
                {
                    AX = memory.Size;
                }

                //int 252 test the byte of memory referenced by ax
                // if ok bx=255
                if (dataBus == 252)
                {
                    BX = 0;
                    if (memory.TestByte(AX)) BX = 255;
                }

                //int 254 clears the display
                if (dataBus == 254)
                {
                    VDU.Clear();
                }

                //int 255 causes a reset
                if (dataBus == 255)
                {
                    Reset();
                }
            }
        }

        private void Start(object sender, System.EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            LoadBios();

            do
            {
                Fetch();

                if (!Decode())
                {
                    UpdateDisplay();
                    break;
                }
                
                UpdateDisplay();

            }
            while (true);
        }

        private void LoadBios()
        {
            byte[] b = BIOS.Load();

            for (int i = 0; i < b.Length; i++)
            {
                memory[i] = b[i];
                IP = 0;
            }
        }
    }
}
