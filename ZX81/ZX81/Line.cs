using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ZX81
{
    class Line
    {
        public string Display { get; set; }

        public Line()
        {
            Display = "";
        }

        public void Clear()
        {
            Display = "";
        }
        
        public void Add (char c)
        {
            Display += c;
        }

        public string[] GetTokens()
        {
            return Display.Split(' ');
        }
    }
}
