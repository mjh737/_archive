using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZX81
{
    public partial class Form1 : Form
    {
        int INPUT_BUFFER = 128;

        Processor cpu = new Processor();
        char[] inputBuffer;
        int index = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeDisplay();
        }

        private void InitializeDisplay()
        {
            Controls.Add(cpu.screen);

            ResetInputBuffer();
        }

        private void ResetInputBuffer()
        {
            inputBuffer = new char[INPUT_BUFFER];
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            
            // User presses return
            if (ch == '\r')
            {
                // Add to code list
                int parseResult = cpu.AddLine(inputBuffer);

                if (parseResult == 0)
                {
                    // Successful so clear buffer
                    ResetInputBuffer();
                    cpu.PrintListing();
                    index = 0;
                }
                if (parseResult == 6)
                {
                    int runResult = cpu.Run();
                    ResetInputBuffer();
                                        
                    index = 0;
                }
                if (parseResult == 8)
                {
                    Close();
                }

                // update screen
            }
            else if (index < INPUT_BUFFER)
            {
                inputBuffer[index] = ch;
                cpu.screen.UpdateScreen(inputBuffer);
                index++;
            }
        }    
    }
}
