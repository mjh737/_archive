using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Graphite
{
    /// <summary>
    /// Represents a BMP Height Map
    /// </summary>
    public class BMPHeightMap
    {
        public string heightMap;
        public int minHeight = 255;
        public int maxHeight = 0;
        public int fileSize;
        public int offset; //Offset to actual data within the BMP file
        public string header; //2 char code - should be BM if alid Bitmap
        public int width; //Width of the heightmap (x)
        public int length;//Length of the heightmap (y)
        /// <summary>
        /// A 2D array containing the height as an integer
        /// </summary>
        public int[,] height;

        /// <summary>
        /// Constructor - Requires full path to a valid bitmap file
        /// </summary>
        /// <param name="pathToHeightmap">Fully qualified path to the BMP file</param>
        public BMPHeightMap(string pathToHeightmap)
        {
            heightMap = pathToHeightmap;
        }

        /// <summary>
        /// Extracts hgeight data from the bitmap file
        /// </summary>
        /// <returns>True if height data is successfully read and stored</returns>
        public bool GetData()
        {
            //Set the filestream and reader
            FileStream stream = new FileStream(heightMap, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            //Check first 2 bytes to make sure this is a BMP file
            header += (char)reader.ReadByte();
            header += (char)reader.ReadByte();
            if (header != "BM") return false;

            //The next 4 bytes are the filesize
            fileSize = reader.ReadByte();
            fileSize += reader.ReadByte() * 256;
            fileSize += reader.ReadByte() * 256 * 256;
            fileSize += reader.ReadByte() * 256 * 256 * 256;

            //The next 4 bytes are always zero so skip them
            reader.ReadBytes(4);

            //The next 4 bytes tell us where the actual offset of the image data
            offset = reader.ReadByte();
            offset += reader.ReadByte() * 256;
            offset += reader.ReadByte() * 256 * 256;
            offset += reader.ReadByte() * 256 * 256 * 256;

            //The next 4 bytes are always 40 so skip them
            reader.ReadBytes(4);

            //The next 4 bytes represent the width
            width = reader.ReadByte();
            width += reader.ReadByte() * 256;
            width += reader.ReadByte() * 256 * 256;
            width += reader.ReadByte() * 256 * 256 * 256;

            //The next 4 bytes represent the height
            length = reader.ReadByte();
            length += reader.ReadByte() * 256;
            length += reader.ReadByte() * 256 * 256;
            length += reader.ReadByte() * 256 * 256 * 256;

            //Now read the bytes we don't want before the data starts
            //(offset less the 26 bytes we've already read)
            reader.ReadBytes(offset - 26);

            //Create the data array
            height = new int[width, length];

            //And fill it using the data from the BMP

            int temp = 0; //Temporary variable to store height calculations

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    temp = (int)(reader.ReadByte());
                    height[width-1-x, length-1-y] = temp;
                    if(temp < minHeight) minHeight = temp; //Get min height
                    if(temp > maxHeight) maxHeight = temp; //Get max height
                }
            }

            return true;
        }
    }
}
