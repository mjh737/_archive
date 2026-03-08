using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ParseStar
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            int counter = 0;

            TextReader reader = new StreamReader(@"C:\Users\mjh\Desktop\stars.dat");

            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(',');

                //HR
                int HR = 0;
                string[] temp = fields[0].Split('=');
                Int32.TryParse(temp[1].TrimStart(' '), out HR);

                //RA
                int RAHours;
                float RAMinutes;
                float RASeconds;
                Int32.TryParse(fields[1], out RAHours);
                float.TryParse(fields[2], out RAMinutes);
                float.TryParse(fields[3], out RASeconds);

                //DEC
                int DECSign = 0;
                int DECDegrees;
                float DECMinutes;
                float DECSeconds;
                if (fields[4].Contains('+')) DECSign = 1;
                if (fields[4].Contains('-')) DECSign = -1;
                Int32.TryParse(fields[4].Substring(1), out DECDegrees);
                float.TryParse(fields[5], out DECMinutes);
                float.TryParse(fields[6], out DECSeconds);

                //MAG
                float MAG;
                float.TryParse(fields[7], out MAG);

                //Spectral Type
                string ST = fields[8].TrimStart();

                //Colour
                string COL = fields[9].TrimStart();

                //Name
                string NAME = fields[10].TrimStart();

                counter++;

                Console.WriteLine(counter + " " + HR + " " + RAHours + " " + RAMinutes + " " + RASeconds + " " + DECSign + " " + DECDegrees + " " + DECMinutes + " " + DECSeconds + " " + MAG + " " + ST + " " + COL + " " + NAME);
            }
            Console.ReadLine();
        }
    }
}
