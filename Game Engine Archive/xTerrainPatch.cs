using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    public class xTerrainPatch
    {
        BoundingBox boundingBox; public BoundingBox BoundingBox { get { return boundingBox; } }
        VertexPositionNormalTexture[] geometry;
        short[] indices;
        VertexBuffer vb;
        IndexBuffer ib;
        int width;
        int depth;
        int offsetX;
        int offsetZ;

        public xTerrainPatch()
        {
            boundingBox = new BoundingBox();
        }

        public void BuildPatch(xHeightMap heightmap, Matrix world, int width, int depth, int offsetX, int offsetZ)
        {
            this.width = width;
            this.depth = depth;

            this.offsetX = offsetX;
            this.offsetZ = offsetZ;

            boundingBox.Min.X = offsetX;
            boundingBox.Min.Z = offsetZ;

            boundingBox.Max.X = offsetX + width;
            boundingBox.Max.Z = offsetZ + depth;

            BuildVertexBuffer(heightmap);

            vb = new VertexBuffer(xGameManager.Instance.Device, VertexPositionNormalTexture.SizeInBytes * geometry.Length, ResourceUsage.WriteOnly, ResourceManagementMode.Automatic);
            vb.SetData<VertexPositionNormalTexture>(geometry);

            BuildIndexBuffer();

            ib = new IndexBuffer(xGameManager.Instance.Device, sizeof(short) * indices.Length, ResourceUsage.WriteOnly, IndexElementSize.SixteenBits);
            ib.SetData<short>(indices);

            // Apply the world matrix transformation to the bounding box.
            boundingBox.Min = Vector3.Transform(boundingBox.Min, world);
            boundingBox.Max = Vector3.Transform(boundingBox.Max, world);
        }

        private void BuildVertexBuffer(xHeightMap heightmap)
        {
            int index = 0;

            Vector3 position;
            Vector3 normal;

            boundingBox.Min.Y = float.MaxValue;
            boundingBox.Max.Y = float.MinValue;

            geometry = new VertexPositionNormalTexture[width * depth];

            for (int z = offsetZ; z < offsetZ + depth; ++z)
            {
                for (int x = offsetX; x < offsetX + width; ++x)
                {
                    float height = heightmap.GetHeightValue(x, z);

                    if (height < boundingBox.Min.Y)
                    {
                        boundingBox.Min.Y = height;
                    }

                    if (height > boundingBox.Max.Y)
                    {
                        boundingBox.Max.Y = height;
                    }

                    position = new Vector3((float)x, height, (float)z);

                    ComputeVertexNormal(heightmap, x, z, out normal);

                    geometry[index] = new VertexPositionNormalTexture(position, normal, new Vector2(x, z));

                    ++index;
                }
            }
        }

        private void BuildIndexBuffer()
        {
            int stripLength = 4 + (depth - 2) * 2;
            int stripCount = width - 1;

            indices = new short[stripLength * stripCount];

            int index = 0;

            for (int s = 0; s < stripCount; ++s)
            {
                for (int z = 0; z < depth; ++z)
                {
                    indices[index] = (short)(s + depth * z);

                    ++index;

                    indices[index] = (short)(s + depth * z + 1);

                    ++index;
                }
            }
        }

        private void ComputeVertexNormal(xHeightMap heightmap, int x, int z, out Vector3 normal)
        {
            int width = heightmap.Width;
            int depth = heightmap.Depth;

            Vector3 center;
            Vector3 p1;
            Vector3 p2;
            Vector3 avgNormal = Vector3.Zero;

            int avgCount = 0;

            bool spaceAbove = false;
            bool spaceBelow = false;
            bool spaceLeft = false;
            bool spaceRight = false;

            Vector3 tempNormal;
            Vector3 v1;
            Vector3 v2;

            center = new Vector3((float)x, heightmap.GetHeightValue(x, z), (float)z);

            if (x > 0) spaceLeft = true;
            if (x < width - 1) spaceRight = true;
            if (z > 0) spaceAbove = true;
            if (z < depth - 1) spaceBelow = true;
            if (spaceAbove && spaceLeft)
            {
                p1 = new Vector3(x - 1, heightmap.GetHeightValue(x - 1, z), z);
                p2 = new Vector3(x - 1, heightmap.GetHeightValue(x - 1, z - 1), z - 1);

                v1 = p1 - center;
                v2 = p2 - p1;

                tempNormal = Vector3.Cross(v1, v2);
                avgNormal += tempNormal;

                ++avgCount;
            }

            if (spaceAbove && spaceRight)
            {
                p1 = new Vector3(x, heightmap.GetHeightValue(x, z - 1), z - 1);
                p2 = new Vector3(x + 1, heightmap.GetHeightValue(x + 1, z - 1), z - 1);

                v1 = p1 - center;
                v2 = p2 - p1;

                tempNormal = Vector3.Cross(v1, v2);
                avgNormal += tempNormal;

                ++avgCount;
            }

            if (spaceBelow && spaceRight)
            {
                p1 = new Vector3(x + 1, heightmap.GetHeightValue(x + 1, z), z);
                p2 = new Vector3(x + 1, heightmap.GetHeightValue(x + 1, z + 1), z + 1);

                v1 = p1 - center;
                v2 = p2 - p1;

                tempNormal = Vector3.Cross(v1, v2);
                avgNormal += tempNormal;

                ++avgCount;
            }

            if (spaceBelow && spaceLeft)
            {
                p1 = new Vector3(x, heightmap.GetHeightValue(x, z + 1), z + 1);
                p2 = new Vector3(x - 1, heightmap.GetHeightValue(x - 1, z + 1), z + 1);

                v1 = p1 - center;
                v2 = p2 - p1;

                tempNormal = Vector3.Cross(v1, v2);
                avgNormal += tempNormal;

                ++avgCount;
            }

            normal = avgNormal / avgCount;
        }

        /// <summary>
        /// Draw the terrain patch.
        /// </summary>
        public void Draw()
        {
            int primitivePerStrip = (depth - 1) * 2;
            int stripCount = width - 1;
            int vertexPerStrip = depth * 2;

            for (int s = 0; s < stripCount; ++s)
            {
                xGameManager.Instance.Device.Vertices[0].SetSource(vb, 0, VertexPositionNormalTexture.SizeInBytes);
                xGameManager.Instance.Device.Indices = ib;
                xGameManager.Instance.Device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, geometry.Length, vertexPerStrip * s, primitivePerStrip);
            }
        }
    }
}
