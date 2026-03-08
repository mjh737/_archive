using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ZX81
{
    public partial class Form1 : Form
    {
        const int screenWidth = 50;
        const int screenHeight = 30;
        const int charWidth = 12;
        const int charHeight = 20;
        Point inputPosition = new Point(0, 500);

        List<Line> lines = new List<Line>();
        Line input = new Line();

        int currentCol = 0;
        int currentRow = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void DrawTestScreen()
        {
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    lines[y].Add('X');
                }
            }

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font f = new Font("Tahoma", charWidth);

            int y;

            for (y = 0; y < lines.Count; y++)
            {
                g.DrawString(lines[y].Display, f, Brushes.Black, new PointF(0, y*charHeight));
            }

            g.DrawString(input.Display, f, Brushes.Black, inputPosition);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter Pressed and line not too short
            if (e.KeyChar == 13)
            {
                if (ParseLine())
                {
                    lines.Add(input);
                    input.Clear();
                    currentRow++;
                }
            }

            if (currentCol < screenWidth)
            {
                input.Add(e.KeyChar);
                currentCol++;
            }

            this.Invalidate();
        }

        private bool ParseLine()
        {
            string[] tokens = input.GetTokens(); ;

            if (tokens.Length < 2) return false;

            return true;
        }
    }
}
