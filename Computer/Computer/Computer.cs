using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Motherboard;
using ComputerControls;

namespace Computer
{
    public partial class Computer : Form
    {
        ByteEntry[] memoryBank = new ByteEntry[8];

        CPU cpu = new CPU();

        public Computer()
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                memoryBank[i] = new ByteEntry();
                memoryBank[i].Location = new Point(50, 30 + (7 - i) * 12);
                memoryBank[i].ForeColor = Color.Red;
                this.Controls.Add(memoryBank[i]);
            }

            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                memoryBank[i].Set(cpu.Memory[i]);
            }

            listBox1.Text = cpu.Output;

            base.OnPaint(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cpu.Mov("ax", 123);
            cpu.Put();

            this.Invalidate(true);
        }
    }
}
