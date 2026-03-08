using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ComputerControls
{
    public partial class Register : UserControl
    {
        string label;
        public string Label { get { return label; } set { label = value; } }

        bool[] bit = new bool[8];
        byte b = new byte();

        public byte Value
        {
            set
            {
                b = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Pen pen = new Pen(Color.Black, 1);

            int fontHeight = 6;
            Font font = new Font("Arial", fontHeight);

            SolidBrush brush = new SolidBrush(Color.Black);
            SolidBrush textBrush = new SolidBrush(Color.Red);

            graphics.DrawRectangle(pen, 0, 0, 80, 20);

            for (int i = 0; i < 8; i++)
            {
                if (bit[7 - i]) graphics.FillRectangle(brush, 2 + i * 8, 12, 7, 7);
                else graphics.DrawRectangle(pen, 2 + i * 8, 12, 6, 6);
            }

            graphics.DrawString(Text , font, textBrush, 2, 2);
            graphics.DrawString(b.ToString(), font, textBrush, 66, 11);
        }

        public override string ToString()
        {
            return b.ToString();
        }
    }
}
