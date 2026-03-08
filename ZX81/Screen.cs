using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZX81
{
    public class Screen : TableLayoutPanel
    {
        const int WIDTH = 32;
        const int HEIGHT = 24;
        const int CHAR_WIDTH = 12;
        const int CHAR_HEIGHT = 20;

        public Screen()
        {
            InitializeScreen();
        }

        private void InitializeScreen()
        {
            ColumnCount = WIDTH;
            RowCount = HEIGHT;
            Location = new Point(13, 13);
            Size = new Size(800, 600);

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    Label cell = new Label();
                    cell.Name = "X" + x + "Y" + y;
                    cell.Width = CHAR_WIDTH;
                    cell.Height = CHAR_HEIGHT;
                    cell.BackColor = Color.Black;
                    cell.ForeColor = Color.White;
                    cell.Padding = new System.Windows.Forms.Padding(0);
                    cell.Margin = new System.Windows.Forms.Padding(0);

                    Controls.Add(cell, x, y);
                }
            }
        }

        public void ClearScreen()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    (Controls.Find("X" + x + "Y" + y, false).Single() as Label).Text = "";
                }
            }
        }

        public int UpdateChar(int x, int y, char ch)
        {
            if (x < 0 || y < 0 || x >= WIDTH || y >= HEIGHT) return 1;

            Label cell = (Controls.Find("X" + x + "Y" + y, false).Single() as Label);
            cell.Text = ch.ToString();

            return 0;
        }

        internal void UpdateScreen(char[] inputBuffer)
        {
            // Clear Screen
            ClearScreen();



            // Update input section
            char[] rowBuffer = new char[32];

            for (int row = 0;row < 4;row++)
            {
                for (int col=0;col<32;col++)
                {
                    rowBuffer[col] = inputBuffer[row * 32 + col];
                    if (col == 31)
                    {
                        UpdateRow(HEIGHT - 3 + row, rowBuffer);
                        rowBuffer = new char[32];
                    }
                }
            }

            // Update code section
        }

        private void UpdateRow(int row, char[] inputBuffer)
        {
            for (int n=0;n<32;n++)
            {
                UpdateChar(n,  row, inputBuffer[n]);
            }
        }
    }
}
