using System;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    //PositionNormalColored Mesh Terrain
    public class sPositionNormalColoredMeshTerrain : SceneObject
    {
        protected ModelMesh mesh;
        protected BMPHeightMap bmp;
        //Material mat;
        protected VertexBuffer vb = null;
        protected IndexBuffer ib = null;

        protected short[] indices;
        protected VertexPositionNormalTexture[] verts = null;

        protected int width;
        protected int length;
        protected int[,] height;


        public sPositionNormalColoredMeshTerrain(GraphicsDevice device)
        {
            bmp = new BMPHeightMap("HeightMap1.bmp");
            ReloadResources(device);
        }

        public override void ReloadResources(GraphicsDevice device)
        {
            
            if (!LoadHeightData()) Debug.WriteLine("No Data Loaded", "Data Loading");

            LoadVertices(device);
            LoadIndices(device);

            mat = new Material();
            mat.Ambient = Color.White;
            mat.Diffuse = Color.White;
            device.Material = mat;

            CreateMesh(device);
        }

        protected void CreateMesh(GraphicsDevice device)
        {
            mesh = new Mesh((width - 1) * (length - 1) * 2, width * length, MeshFlags.Managed, CustomVertex.PositionNormalColored.Format, device);
            mesh.SetVertexBufferData(verts, LockFlags.None);
            mesh.SetIndexBufferData(indices, LockFlags.None);
            mesh.ComputeNormals();
            OptimizeMesh();
        }

        protected void OptimizeMesh()
        {
            int[] adj = new int[mesh.NumberFaces*3];
            mesh.GenerateAdjacency(0.5f, adj);
            mesh.OptimizeInPlace(MeshFlags.OptimizeVertexCache, adj);
        }

        protected void LoadVertices(GraphicsDevice device)
        {
            vb = new VertexBuffer(typeof(CustomVertex.PositionNormalColored), width * length, device, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionNormalColored.Format, Pool.Default);
            verts = new CustomVertex.PositionNormalColored[width*length];

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

        protected void LoadIndices(GraphicsDevice device)
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

        public override int Render(GraphicsDevice device)
        {
            int numSubsets = mesh.GetAttributeTable().Length;
            for (int i = 0; i < numSubsets; i++)
            {
                mesh.DrawSubset(i);
            }

            return mesh.NumberFaces;
        }
    }
}
