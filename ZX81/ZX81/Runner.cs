namespace ZX81
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using static ZX81.Enums;

    internal class Runner
    {
        private Form1 _form;

        public Runner(Form1 form)
        {
            _form = form;
        }

        internal string Run(List<Line> listing)
        {
            _form.Display.Clear();  

            foreach (var line in listing)
            {
                if (line.Tokens[1].TokenType == TokenType.Keyword)
                {
                    if (line.Tokens[1].Identifier == Keyword.PRINT.ToString())
                    {
                        _form.Display.Text += line.Tokens[2].Value;
                    }
                }
            }

            return "";
        }
    }
}
