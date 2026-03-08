using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        string displayValue;
        decimal runningTotal;
        decimal memory;
        bool isFraction;
        bool canOverwrite;
        bool isAcceptingOperator;
        Operator currentOperator;
        

        public Form1()
        {
            InitializeComponent();
            ResetDisplay(this, null);
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            while (displayValue.EndsWith("0"))
            {
                displayValue = displayValue.Substring(0, displayValue.Length - 1);
            }

            Display.Text = displayValue;

            

            if (memory == 0)
                MTextBox.Text = "";
            else
                MTextBox.Text = "M";

            Display.ReadOnly = true;
            Display.BackColor = Color.White;
            Display.ForeColor = Color.Black;

            Display.DeselectAll();
        }

        private void ClickNumber(object sender, EventArgs e)
        {
            isAcceptingOperator = true;

            Button btn = sender as Button;

            if (canOverwrite)
            {
                canOverwrite = false;
                displayValue = btn.Text + ".";
                UpdateDisplay();
                return;
            }

            if (isFraction) displayValue += btn.Text;
            else
            {
                displayValue = displayValue.Substring(0, displayValue.Length - 1);
                displayValue += btn.Text + ".";

                if (displayValue.StartsWith("0"))
                    displayValue = displayValue.Substring(1);
            }


            UpdateDisplay();
        }

        private void buttonNegate_Click(object sender, EventArgs e)
        {
            if (displayValue == "0.") return;

            
        
            if (displayValue.StartsWith("-"))
                displayValue = displayValue.Substring(1);
            else
                displayValue = "-" + displayValue;

            UpdateDisplay();

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (canOverwrite) return;

            if (isFraction)
            {
                if (displayValue.EndsWith("."))
                    isFraction = false;
                else displayValue = displayValue.Remove(displayValue.Length - 1);
            }
            else
            {
                displayValue = displayValue.Substring(0, displayValue.Length - 2);
                displayValue += ".";

                if (displayValue == ".") displayValue = "0.";
            }

            UpdateDisplay();

        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (canOverwrite)
            {
                displayValue = "0.";
                isFraction = true;
                canOverwrite = false;
                UpdateDisplay();
                return;
            }

            if (isFraction == false) isFraction = true;
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            displayValue = "0.";
            isFraction = false;

            UpdateDisplay();
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            if (displayValue == "0.") return;

            memory += ParseDisplay();

            UpdateDisplay();
        }

        private decimal ParseDisplay()
        {
            decimal d;
            if (!decimal.TryParse(displayValue, out d))
                throw new Exception("Invalid Display");

            return d;
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            memory = 0m;

            UpdateDisplay();
        }

        private void buttonMS_Click(object sender, EventArgs e)
        {
            memory -= ParseDisplay();

            UpdateDisplay();
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            displayValue = memory.ToString();

            if (displayValue.Contains(".")) isFraction = true;
            else displayValue += ".";

            canOverwrite = true;
            isAcceptingOperator = true;

            UpdateDisplay();
        }

        private void ResetDisplay(object sender, EventArgs e)
        {
            displayValue = "0.";
            runningTotal = 0m;
            isFraction = false;
            canOverwrite = false;
            isAcceptingOperator = true;
            currentOperator = Operator.Null;
        }

         private void FormatDisplayValue()
        {
            displayValue = runningTotal.ToString();

            if (!displayValue.Contains("."))
                displayValue += ".";
        }

        private void BasicOperatorClick(object sender, EventArgs e)
        {
            if (!isAcceptingOperator) return;

            Button btn = sender as Button;

            switch (currentOperator)
            {
                case Operator.Null:
                    runningTotal += ParseDisplay();
                    break;

                case Operator.Add:
                    runningTotal += ParseDisplay();
                    FormatDisplayValue();
                    break;

                case Operator.Subtract:
                    runningTotal -= ParseDisplay();
                    FormatDisplayValue();
                    break;

                case Operator.Multiply:
                    runningTotal *= ParseDisplay();
                    FormatDisplayValue();
                    break;

                case Operator.Divide:
                    runningTotal /= ParseDisplay();
                    FormatDisplayValue();
                    break;
            }

            isAcceptingOperator = false;

            currentOperator = Operator.Null;
            if (btn.Text == "+") currentOperator = Operator.Add;
            if (btn.Text == "-") currentOperator = Operator.Subtract;
            if (btn.Text == "x") currentOperator = Operator.Multiply;
            if (btn.Text == "/") currentOperator = Operator.Divide;

            if (btn.Text == "=")
            {
                FormatDisplayValue();
                currentOperator = Operator.Null;
                runningTotal = 0m;
                isAcceptingOperator = true;
            }

            canOverwrite = true;

            UpdateDisplay();

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, Display.Text);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            decimal d = 0m;
            decimal.TryParse(Clipboard.GetData(DataFormats.Text).ToString(), out d);

            displayValue = d.ToString();
        }

        private void aboutCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1' ||
                e.KeyChar == '2' ||
                e.KeyChar == '3' ||
                e.KeyChar == '4' ||
                e.KeyChar == '5' ||
                e.KeyChar == '6' ||
                e.KeyChar == '7' ||
                e.KeyChar == '8' ||
                e.KeyChar == '9' ||
                e.KeyChar == '0')
            {
                isAcceptingOperator = true;

                Button btn = sender as Button;

                if (canOverwrite)
                {
                    canOverwrite = false;
                    displayValue = e.KeyChar + ".";
                    UpdateDisplay();
                    return;
                }

                if (isFraction) displayValue += btn.Text;
                else
                {
                    displayValue = displayValue.Substring(0, displayValue.Length - 1);
                    displayValue += e.KeyChar + ".";

                    if (displayValue.StartsWith("0"))
                        displayValue = displayValue.Substring(1);
                }


                UpdateDisplay();
            }
        }
    }

    enum Operator
    {
        Null,
        Add,
        Subtract,
        Multiply,
        Divide,
        SquareRoot
    }
}
