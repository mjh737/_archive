namespace ZX81
{
    using System;
    using System.Collections.Generic;
    using static ZX81.Enums;

    internal class Lexer
    {
        public static List<string> Keywords = new List<string>()
        { Keyword.INT.ToString(), Keyword.STRING.ToString(), Keyword.PRINT.ToString()
        , Keyword.LET.ToString(), Keyword.GOTO.ToString(), Keyword.GOSUB.ToString()};

        public Line Parse(string input)
        {
            string[] parts = input.Split(" ");

            if (parts.Length < 2)
                return null;

            List<Token> tokens = new List<Token>();

            if (!int.TryParse(parts[0], out int lineNumber))
                return null;

            tokens.Add(new Token(TokenType.LineNumber, parts[0]));

            bool inString = false;
            string str = "";

            foreach (string part in parts[1..])
            {
                if (part.StartsWith("\"") && part.EndsWith("\""))
                    tokens.Add(new Token(TokenType.String, part));
                else if (part.StartsWith("\"") || inString)
                {
                    inString = true;
                    str += part.Substring(1) + " ";
                }
                else if (part.EndsWith("\""))
                {
                    str += part.Substring(0, part.Length - 1);
                    tokens.Add(new Token(TokenType.String, str));
                    str = "";
                }
                else if (part.Length == 1 && !IsNumber(part))
                    tokens.Add(new Token(TokenType.Variable, part));
                else if (Keywords.Contains(part))
                    tokens.Add(new Token(TokenType.Keyword, part));
                else if (IsNumber(part))
                    tokens.Add(new Token(TokenType.Number, part));

                else
                    throw new Exception("Unknown Token");
            }

            return new Line(tokens);
        }

        private bool IsNumber(string part)
        {
            return float.TryParse(part, out float _);
        }
    }
}
