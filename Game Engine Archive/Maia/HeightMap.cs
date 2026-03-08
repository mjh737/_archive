using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Graphite
{
    public class HeightMap
    {
        public int HEIGHT;
        public int WIDTH;
        public int[,] heightData;

        public HeightMap(string pathToHeightMap)
	    {
            int offset;
            FileStream fs = new FileStream(pathToHeightMap, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);

            r.BaseStream.Seek(10, SeekOrigin.Current);
            offset = (int)r.ReadUInt32();

            r.BaseStream.Seek(4, SeekOrigin.Current);
            WIDTH = (int)r.ReadUInt32();
            HEIGHT = (int)r.ReadUInt32();

            r.BaseStream.Seek(offset-26, SeekOrigin.Current);
            heightData = new int[WIDTH, HEIGHT];
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int y = 0; y < WIDTH; y++)
                {
                    int height = (int)(r.ReadByte());
                    height += (int)(r.ReadByte());
                    height += (int)(r.ReadByte());
                    height /= 8;
                    heightData[WIDTH - 1 - y, HEIGHT - 1 - i] = height;
	            }
            }
        }
    }
}