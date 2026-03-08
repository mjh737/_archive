using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC2
{
    public class VDU
    {
        static string data;

        public static string GetScreen()
        {
            return data;
        }

        public static void Print(string message)
        {
            data += message;
        }

        public static void Clear()
        {
            data = "";
        }


    }
}
