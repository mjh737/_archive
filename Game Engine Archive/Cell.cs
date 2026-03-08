using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Graphite
{
    public class Cell : SceneObject
    {
        int heightScalingFactor = 4;
        public int numVertsPerSide;
        public int numVertsTotal;
        public int numQuadsPerSide;
        int numQuadsTotal;
        public int numTrianglesTotal;
        int numIndices; //Indices

        private Device device;
        private IndexBuffer ib = null;
        private VertexBuffer vb = null;
        private Texture terrainTexture;
        protected int[] indices;
        protected CustomVertex.PositionNormalTextured[] verts = null;

        int[,] elevation;

        public int XOffset; // xPosition in the world of this cell's origin
        public int ZOffset; // yPosition in the world of this cell's origin

        public Cell(Device device, int dX, int dZ, int numQuadsPerCellSide)
        {
            this.device = device;
            this.XOffset = dX;
            this.ZOffset = dZ;
            this.numQuadsPerSide = numQuadsPerCellSide;
            terrainTexture = TextureLoader.FromFile(device, @"Textures\LANDFILL.jpg");
            ComputeValues();

            BMPHeightMap bmp = new BMPHeightMap(@"HeightMaps\current.bmp");
            bmp.GetData();
            elevation = bmp.height;

            LoadVertexBuffer();
            LoadIndexBuffer();
        }

        private void ComputeValues()
        {
            numVertsPerSide = numQuadsPerSide + 1;
            numVertsTotal = numVertsPerSide * numVertsPerSide;
            numQuadsTotal = numQuadsPerSide * numQuadsPerSide;
            numTrianglesTotal = numQuadsTotal * 2;
            numIndices = numQuadsTotal * 6;
        }

        private void LoadVertexBuffer()
        {
            vb = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured), numVertsTotal, device, Usage.WriteOnly, CustomVertex.PositionNormalTextured.Format, Pool.Managed);
            verts = new CustomVertex.PositionNormalTextured[numVertsTotal];

            for (int z = 0; z < numVertsPerSide; z++)
            {
                for (int x = 0; x < numVertsPerSide; x++)
                {
                    CustomVertex.PositionNormalTextured vertex;
                    vertex.X = x + XOffset;
                    vertex.Y = elevation[XOffset + x, ZOffset + z] / heightScalingFactor;
                    vertex.Z = z + ZOffset;

                    vertex.Tu = (float)x / numQuadsPerSide;
                    vertex.Tv = (float)z / numQuadsPerSide;

                    //Setup a bogus normal
                    vertex.Nx = 0;
                    vertex.Ny = 1;
                    vertex.Nz = 0;

                    verts[(z * numVertsPerSide) + x] = vertex;
                }
            }

            ComputeNormals();

            vb.SetData(verts, 0, LockFlags.None);
        }

        private void ComputeNormals()
        {
            for (int z = 1; z < numQuadsPerSide; z++)
            {
                for (int x = 1; x < numQuadsPerSide; x++)
                {
                    Vector3 X = Vector3.Subtract(
                    verts[z * numVertsPerSide + x + 1].Position,
                    verts[z * numVertsPerSide + x - 1].Position);
                    
                    Vector3 Z = Vector3.Subtract(
                    verts[(z + 1) * numVertsPerSide + x].Position,
                    verts[(z - 1) * numVertsPerSide + x].Position);

                    Vector3 normal = Vector3.Cross(Z, X);
                    normal.Normalize();
                    verts[(z * numVertsPerSide) + x].Normal = normal;
                }
            }
        }

        public void LoadIndexBuffer()
        {
            int numIndices = (numVertsPerSide * 2) * (numQuadsPerSide) + (numVertsPerSide - 2);
            indices = new int[numIndices];

            ib = new IndexBuffer(typeof(int), indices.Length, device, Usage.WriteOnly, Pool.Managed);

            int index = 0;

            for (int z = 0; z < numQuadsPerSide; z++)
            {
                if (z % 2 == 0)
                {
                    int x;
                    for (x = 0; x < numVertsPerSide; x++)
                    {
                        indices[index++] = x + (z * numVertsPerSide);
                        indices[index++] = x + (z * numVertsPerSide) + numVertsPerSide;
                    }
                    if (z != numVertsPerSide - 2)
                    {
                        indices[index++] = --x + (z * numVertsPerSide);
                    }
                }
                else
                {
                    int x;
                    for (x = numVertsPerSide -1; x>=0; x--)
                    {
                        indices[index++] = x + (z * numVertsPerSide);
                        indices[index++] = x + (z * numVertsPerSide) + numVertsPerSide;
                    }
                    if (z != numVertsPerSide - 2)
                    {
                        indices[index++] = ++x + (z * numVertsPerSide);
                    }
                }
            }

            ib.SetData(indices, 0, 0);
        }

        public override int Render(Device device)
        {
            device.SetTexture(0, terrainTexture);
            device.Indices = ib;
            device.SetStreamSource(0, vb, 0);
            device.VertexFormat = CustomVertex.PositionNormalTextured.Format;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, numVertsTotal, 0, indices.Length - 2);

            return indices.Length - 2;
        }

        public int GetHeight(int x, int z)
        {
            if(x < 0 | x > numQuadsPerSide + XOffset | z < 0 | z > numQuadsPerSide + ZOffset) 
                return int.MinValue;
            else
            {
                return elevation[x, z] / heightScalingFactor;
            }
        }

    }
}

