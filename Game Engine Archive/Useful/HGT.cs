using System;
using System.IO;

namespace analyzeHGT
{
    public partial class test
    {
        static void Main()
        {
            int[] number = new int[100000];
            float last = 0;

            StreamReader stm = new StreamReader(@"C:\AAAA\N53E000.txt");
            string line;
            int index = 0;
            float conv;

            string[] point = new string[3];

            line = stm.ReadLine();
            point = line.Split(',');

            float.TryParse(point[2], out last);
            number[index]++;
            index++;

            while ((line = stm.ReadLine()) != null)
            {
                point = line.Split(',');

                float.TryParse(point[2].Trim(), out conv);

                if (conv == last)
                {
                    number[index]++;
                }
                else
                {
                    Console.WriteLine(number[index-1]);
                    last = conv;
                    index++;
                }

            }

            Console.ReadKey();
        }
    }
}