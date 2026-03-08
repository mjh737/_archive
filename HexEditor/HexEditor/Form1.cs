using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HexEditor
{
    public partial class Form1 : Form
    {
        byte[,] bytes;
        string file = @"C:\install.ini";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream stm = File.Open(file, FileMode.Open);
            long len = stm.Length;

            bytes = new byte [len/8 + 1,8];

            for (int i = 0; i < len; i++)
            {
                bytes[i/8,i%8] = (byte)stm.ReadByte();
            }

            richTextBox1.Text = "\t01\t02\t03\t04\t05\t06\t07\t08\n";

            for (int row = 0; row < len/8 + 1; row++)
            {
                richTextBox1.Text += row.ToString() + "\t";

                for (int col = 0;col < 8;col++)
                {
                    richTextBox1.Text += bytes[row, col].ToString() + "\t";
                }

                richTextBox1.Text += "\n";
            }


        }
    }
}
