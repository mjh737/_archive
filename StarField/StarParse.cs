using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace StarField
{
    public class StarParse
    {
        int numStars = 9096; public int NumStars { get { return numStars; } }
        int count = 0;
        string filename;
        Star[] stars; public Star[] Stars { get { return stars; } }

        public StarParse(String filePath)
        {
             filename = filePath;
             stars = new Star[numStars];
             ParseFile();
        }

        public void ParseFile()
        {
            TextReader reader = new StreamReader(filename);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                ParseLine(line);
            }

            reader.Close();
        }

        private void ParseLine(string line)
        {
            if (line == "") return;
            if (line.StartsWith("[")) ProcessHeading(line);
            if (line.StartsWith("Star.")) ProcessStar(line);
        }

        private void ProcessHeading(string line)
        {
        }

        private void ProcessStar(string line)
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
            if (fields[4].Contains("+")) DECSign = 1;
            if (fields[4].Contains("-")) DECSign = -1;
            Int32.TryParse(fields[4].Substring(1), out DECDegrees);
            float.TryParse(fields[5], out DECMinutes);
            float.TryParse(fields[6], out DECSeconds);

            //MAG
            float MAG;
            float.TryParse(fields[7], out MAG);

            //Spectral Type
            string ST = fields[8].TrimStart();

            //Colour
            string color = fields[9].TrimStart();
            string[] xCol = new string[4];
            xCol[0] = color.Substring(2, 2);
            xCol[1] = color.Substring(4, 2);
            xCol[2] = color.Substring(6, 2);
            xCol[3] = color.Substring(8, 2);
            
            byte[] bCol = new byte[4];
            bCol[0] = byte.Parse(xCol[0], NumberStyles.HexNumber);
            bCol[1] = byte.Parse(xCol[1], NumberStyles.HexNumber);
            bCol[2] = byte.Parse(xCol[2], NumberStyles.HexNumber);
            bCol[3] = byte.Parse(xCol[3], NumberStyles.HexNumber);
            Color COL = new Color((bCol[3]), bCol[2], bCol[1], bCol[0]);

            //Name
            string NAME = fields[10].TrimStart();

            stars[count] = (new Star("Star." + count, new Ascension(RAHours, RAMinutes + (RASeconds / 60)), new Declination(DECSign, DECDegrees, DECMinutes + (DECSeconds / 60)), COL, MAG));

            ////Dipper
            //if (count == 5191) stars[count].Color = Color.Red;
            //if (count == 5054) stars[count].Color = Color.Red;
            //if (count == 4905) stars[count].Color = Color.Red;
            //if (count == 4660) stars[count].Color = Color.Red;
            //if (count == 4554) stars[count].Color = Color.Red;
            //if (count == 4295) stars[count].Color = Color.Red;
            //if (count == 4301) stars[count].Color = Color.Red;
            //if (count == 4660) stars[count].Color = Color.Red;

            ////Orion's Belt
            //if (count == 1852) stars[count].Color = Color.Yellow;
            //if (count == 1903) stars[count].Color = Color.Yellow;
            //if (count == 1948) stars[count].Color = Color.Yellow;

            count++;
        }
    }
}
