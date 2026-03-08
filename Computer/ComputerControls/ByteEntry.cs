using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ComputerControls
{
    public partial class ByteEntry : UserControl
    {
        bool[] bit = new bool[8];
        byte b = new byte();

        public ByteEntry() : this(0)
        {

        }

        public ByteEntry(byte b)
        {
            Set(b);
        }

        public void Set(byte value)
        {
            bit[0] = (value & 1) == 1;
            bit[1] = (value & 2) == 2;
            bit[2] = (value & 4) == 4;
            bit[3] = (value & 8) == 8;
            bit[4] = (value & 16) == 16;
            bit[5] = (value & 32) == 32;
            bit[6] = (value & 64) == 64;
            bit[7] = (value & 128) == 128;

            this.b = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Pen pen = new Pen(Color.Black, 1);

            int fontHeight = 6;
            Font font = new Font("Arial", fontHeight);
            SolidBrush brush = new SolidBrush(this.ForeColor);

            graphics.DrawRectangle(pen, 0, 0, 57, 8);

            for (int i = 0; i < 8; i++)
            {
                if (bit[7 - i]) graphics.FillRectangle(brush, 1 + i * 7, 1, 7, 7);
            }

            graphics.DrawString(b.ToString(), font, brush, 59, 0);
        }
    }
}
