using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZX81
{
    public class Processor
    {
        // Error Codes
        // ===========
        // 0 Success
        // 1 Nothing to do
        // 2 Clear Screen
        // 3 No line number
        // 4 Missing Argument
        // 5 Syntax Error
        // 6 Run
        // 7 Overflow
        // 8 Exit
        // 9 Awaiting key press


        public Screen screen = new Screen();

        Dictionary<int, string> listing = new Dictionary<int, string>();

        int cursorX, cursorY;

        public int AddLine(char[] buffer)
        {
            StringBuilder str = new StringBuilder();

            foreach (char ch in buffer)
            {
                if (ch == '\0') break;
                str.Append(ch);
            }

            string line = str.ToString();

            // If empty return false
            if (string.IsNullOrEmpty(line)) return 1;

            // split the line up
            string[] parts = line.Split(' ');
            int numParts = parts.Count();

            // if there aren't two parts then it's only valid if it's CLS
            if (numParts == 1)
            {
                if (parts[0].ToUpper() == "CLS") return 2;
                if (parts[0].ToUpper() == "RUN") return 6;
                if (parts[0].ToUpper() == "EXIT") return 8;
            }

            // if the first part is not a number then return
            int lineNumber;
            if(!int.TryParse(parts[0], out lineNumber)) return 3;

            // test for commands
            switch(parts[1].ToUpper())
            {
                case "PRINT":
                {
                    // Make sure there is something to print
                    if (numParts < 3) return 4;

                    if (parts[2].StartsWith("\"") && parts[2].EndsWith("\""))
                    {
                        listing.Add(lineNumber, parts[1].ToUpper() + " " + parts[2]);
                        return 0;
                    }
                    else return 5;
                }
                default:
                return 5;
            }
        }

        public int Run()
        {
            // Make sure there's something to run
            if (listing.Count() < 1) return 1;

            foreach (var line in listing.OrderBy(l => l.Key))
            {
                string command = line.Value;

                string[] parts = command.Split(' ');

                if (parts[0].ToUpper() == "PRINT")
                {
                    foreach (char ch in parts[1].ToArray())
                    {
                        if (ch == '\"') continue;

                        screen.UpdateChar(cursorX, cursorY, ch);
                        cursorX++;
                        if (cursorX >= 32)
                        {
                            cursorX = 0;
                            cursorY++;
                        }

                        if (cursorY >= 20) return 7;
                    }
                }
            }

            PrintListingLine(21, "Finished");

            return 9;
        }

        internal void PrintListing()
        {
            screen.ClearScreen();

            int row = 0;

            foreach (var line in listing)
            {
                PrintListingLine(row, line.Key.ToString() + " " + line.Value);
                row++;
            }
        }

        void PrintListingLine(int row, string text)
        {
            int col = 0;

            foreach (var ch in text.ToCharArray())
            {
                screen.UpdateChar(col, row, ch);
                col++;
            }
        }
    }
}
