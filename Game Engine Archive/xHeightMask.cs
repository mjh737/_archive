using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Graphite
{
    public class xHeightMask
    {
        int width; public int Width { get { return width; } }
        int depth; public int Depth { get { return Depth; } }
        float[] mask; public float[] Mask { get { return mask; } }

        public xHeightMask()
        {
            width = 0;
            depth = 0;
            mask = null;
        }

        public xHeightMask(int width, int depth, float[] data)
        {
            this.width = width;
            this.depth = depth;

            mask = (float[])data.Clone();
        }

        public float GetMask(int x, int z)
        {
            return mask[x + z * width];
        }

        public void SetMask(int x, int z, float value)
        {
            mask[x + z * width] = value;
        }

        public void SaveToFile(String path)
        {
            try
            {
                FileStream stream = File.Open(path, FileMode.OpenOrCreate);

                if (stream != null)
                {
                    BinaryWriter writer = new BinaryWriter(stream);

                    writer.Write(width);
                    writer.Write(depth);

                    for (int i = 0; i < mask.Length; ++i)
                    {
                        writer.Write(mask[i]);
                    }

                    writer.Flush();

                    stream.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        public void LoadFromFile(String path)
        {
            try
            {
                FileStream stream = File.Open(path, FileMode.Open);

                BinaryReader reader = new BinaryReader(stream);

                width = reader.ReadInt32();
                depth = reader.ReadInt32();

                mask = new float[width * depth];

                for (int i = 0; i < mask.Length; ++i)
                {
                    mask[i] = reader.ReadSingle();
                }

                reader.Close();

                stream.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
