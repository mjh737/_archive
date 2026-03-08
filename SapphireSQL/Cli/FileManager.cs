namespace Cli
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Text;

    internal class FileManager
    {
        private const string filepath = "output.sdb";

        public void WriteRow(List<string> columns)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memoryStream);

            foreach (var column in columns)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(column);
                writer.Write((ushort)bytes.Length);
                writer.Write(bytes);
            }

            byte[] header = new byte[32];
            Encoding.UTF8.GetBytes("SSQL", 0, 4, header, 0);
            header[4] = 1; // Version
            header[5] = (byte)(memoryStream.Length & 0xFF);
            header[6] = (byte)((memoryStream.Length >> 8) & 0xFF);
            header[7] = (byte)((memoryStream.Length >> 16) & 0xFF);
            header[8] = (byte)((memoryStream.Length >> 24) & 0xFF);
            header[9] = (byte)((memoryStream.Length >> 32) & 0xFF);
            header[10] = (byte)((memoryStream.Length >> 40) & 0xFF);
            header[11] = (byte)((memoryStream.Length >> 48) & 0xFF);
            header[12] = (byte)((memoryStream.Length >> 56) & 0xFF);

            byte[] file = new byte[32 + memoryStream.Length];
            Array.Copy(header, 0, file, 0, header.Length);
            Array.Copy(memoryStream.ToArray(), 0, file, header.Length, memoryStream.Length);

            File.AppendAllBytes(filepath, file);
        }
    }
}
