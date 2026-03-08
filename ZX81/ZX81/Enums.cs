namespace ZX81
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Enums
    {
        public enum TokenType
        {
            LineNumber,
            Variable,
            Keyword,
            Number,
            String
        }

        public enum Keyword
        {
            CLS,
            INT,
            LET,
            FOR,
            GOTO,
            GOSUB,
            STRING,
            BREAK,
            PRINT,
            INPUT,
            RUN,
            SAVE,
            LOAD
        }

        
    }
}
