using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using PC2;

namespace Compiler
{
    public partial class Form1 : Form
    {
        Queue<byte> program;

        public Form1()
        {
            InitializeComponent();
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            program = new Queue<byte>();

            string[] rows;

            rows = Input.Text.Split('\n');

            foreach (string line in rows)
            {
                string verb = "", op1 = "", op2 = "";
                string[] temp = line.Split(' ', ',');

                if (temp.Length == 0) continue;
                
                if(temp.Length == 1)
                    verb = temp[0];
                if (temp.Length == 2)
                {
                    verb = temp[0];
                    op1 = temp[1];
                }
                if (temp.Length == 3)
                {
                    verb = temp[0];
                    op1 = temp[1];
                    op2 = temp[2];
                }

                Interpret(verb, op1, op2);
            }

            DisplayProgram();

            
        }

        private void DisplayProgram()
        {
            Output.Text = "";

            foreach (byte b in program)
            {
                Output.Text += b + ",";
            }

            if (Output.Text.Length > 0)
                Output.Text = Output.Text.Substring(0, Output.Text.Length - 1);
        }

        private void Interpret(string verb, string op1, string op2)
        {
            switch (verb)
            {
                case "cls":
                    {
                        program.Enqueue(7);
                        program.Enqueue(254);
                        break;
                    }

                case "print":
                    {
                        program.Enqueue(7);
                        program.Enqueue(1);

                        foreach (char c in op1)
                        {
                            if (c == '\"') continue;
                            program.Enqueue(Converter.ConvertCharToByte(c));
                        }

                        break;
                    }

                case "mov":
                    {
                        if (op1 == "ax")
                        {
                            if (op2 == "ax") program.Enqueue(Converter.ConvertBitsToByte("11000000"));
                            if (op2 == "bx") program.Enqueue(Converter.ConvertBitsToByte("11000001"));
                            if (op2 == "cx") program.Enqueue(Converter.ConvertBitsToByte("11000011"));
                            if (op2 == "dx") program.Enqueue(Converter.ConvertBitsToByte("11000011"));

                        }

                        if (op1 == "bx")
                        {
                            if (op2 == "ax") program.Enqueue(Converter.ConvertBitsToByte("11001000"));
                            if (op2 == "bx") program.Enqueue(Converter.ConvertBitsToByte("11001001"));
                            if (op2 == "cx") program.Enqueue(Converter.ConvertBitsToByte("11001011"));
                            if (op2 == "dx") program.Enqueue(Converter.ConvertBitsToByte("11001011"));
                        }

                        if (op1 == "cx")
                        {
                            if (op2 == "ax") program.Enqueue(Converter.ConvertBitsToByte("11010000"));
                            if (op2 == "bx") program.Enqueue(Converter.ConvertBitsToByte("11010001"));
                            if (op2 == "cx") program.Enqueue(Converter.ConvertBitsToByte("11010011"));
                            if (op2 == "dx") program.Enqueue(Converter.ConvertBitsToByte("11010011"));
                        }

                        if (op1 == "dx")
                        {
                            if (op2 == "ax") program.Enqueue(Converter.ConvertBitsToByte("11011000"));
                            if (op2 == "bx") program.Enqueue(Converter.ConvertBitsToByte("11011001"));
                            if (op2 == "cx") program.Enqueue(Converter.ConvertBitsToByte("11011011"));
                            if (op2 == "dx") program.Enqueue(Converter.ConvertBitsToByte("11011011"));
                        }

                        break;
                    }

                default:
                    break;
            }
        }
    }
}
