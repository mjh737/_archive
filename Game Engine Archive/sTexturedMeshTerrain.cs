using System;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;
using System.Diagnostics;

namespace Sapphire
{
    public class sTexturedMeshTerrain : sObject
    {
        protected Mesh mesh;
        protected sBMPHeightMap bmp;
        Material mat;
        private Texture texture;
        string textureLocation;
        protected VertexBuffer vb = null;
        protected IndexBuffer ib = null;

        protected short[] indices;
        protected CustomVertex.PositionColored[] verts = null;

        protected int width;
        protected int length;
        protected int[,] height;


        public sTexturedMeshTerrain(Device device, string heightMap, string textureLocation)
        {
            this.textureLocation = textureLocation;
            bmp = new sBMPHeightMap(heightMap);
            ReloadResources(device);
        }

        public override void ReloadResources(Device device)
        {
            
            if (!LoadHeightData()) Debug.WriteLine("No Data Loaded", "Data Loading");

            LoadVertices(device);
            LoadIndices(device);

            mat = new Material();
            mat.Ambient = Color.White;
            mat.Diffuse = Color.White;
            device.Material = mat;

            texture = TextureLoader.FromFile(device, textureLocation);
            device.SetTexture(0, texture);

            CreateMesh(device);
        }

        protected void CreateMesh(Device device)
        {
            mesh = new Mesh((width - 1) * (length - 1) * 2, width * length, MeshFlags.Managed, CustomVertex.PositionColored.Format, device);
            mesh.SetVertexBufferData(verts, LockFlags.None);
            mesh.SetIndexBufferData(indices, LockFlags.None);

            OptimizeMesh();
        }

        protected void OptimizeMesh()
        {
            int[] adj = new int[mesh.NumberFaces*3];
            mesh.GenerateAdjacency(0.5f, adj);
            mesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adj);
        }

        protected void LoadVertices(Device device)
        {
            vb = new VertexBuffer(typeof(CustomVertex.PositionColored), width * length, device, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            verts = new CustomVertex.PositionColored[width*length];

            for(int x=0; x< width; x++)
            {
                for(int y=0; y<length ;y++)
                {
                    verts[x+y*width].Position = new Vector3(x,y,height[x,y]);
                    verts[x+y*width].Color = Color.White.ToArgb();
                }
            }

            vb.SetData(verts, 0, LockFlags.None);
        }

        protected void LoadIndices(Device device)
        {
            ib = new IndexBuffer(typeof(short), (width - 1) * (length - 1) * 6, device, Usage.WriteOnly, Pool.Default);
            indices = new short[(width - 1) * (length - 1) * 6];

            for (int x = 0; x < width-1; x++)
            {
                for (int y = 0; y < length-1; y++)
                {
                    indices[(x + y * (width - 1)) * 6] = (short)((x + 1) + (y + 1) * width);
                    indices[(x + y * (width - 1)) * 6 + 1] = (short)((x + 1) + y * width);
                    indices[(x + y * (width - 1)) * 6 + 2] = (short)(x + y * width);
                    indices[(x + y * (width - 1)) * 6 + 3] = (short)((x + 1) + (y + 1) * width);
                    indices[(x + y * (width - 1)) * 6 + 4] = (short)(x + y * width);
                    indices[(x + y * (width - 1)) * 6 + 5] = (short)(x + (y + 1) * width);
                }
            }

            ib.SetData(indices, 0, LockFlags.None);
        }

        protected bool LoadHeightData()
        {
            if (bmp.GetData())
            {
                height = bmp.height;
                length = bmp.length;
                width = bmp.width;
                return true;
            }
            return false;
        }

        public override void Render(Device device)
        {
            int numSubsets = mesh.GetAttributeTable().Length;
            for (int i = 0; i < numSubsets; i++)
            {
                mesh.DrawSubset(i);
            }
        }
    }
}
