using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Graphite
{
    public class Quad
    {
        float size = 10;

        private string asset;
        private Texture2D texture;
        VertexPositionTexture[] vertices;
        int[] indices;
        private VertexBuffer vb;
        private IndexBuffer ib;
        VertexDeclaration vertexDeclaration;

        public Quad(string content, Vector3[] vectors)
        {
            if (vectors.Length != 4) throw new IndexOutOfRangeException("More than 4 vertices passed to Quad");

            vertices = new VertexPositionTexture[4];

            vertices[0] = new VertexPositionTexture(vectors[0] * size, new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(vectors[1] * size, new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(vectors[2] * size, new Vector2(0, 1));
            vertices[3] = new VertexPositionTexture(vectors[3] * size, new Vector2(1, 1));

            indices = new int[6] { 0, 1, 2, 1, 3, 2 };

            this.asset = content;

            
        }

         public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(asset) as Texture2D;
        }

         public void Draw(GraphicsDevice graphicsDevice)
         {
             vertexDeclaration = new VertexDeclaration(graphicsDevice, VertexPositionTexture.VertexElements);

             vb = new VertexBuffer(graphicsDevice, VertexPositionTexture.SizeInBytes * 4, BufferUsage.None);
             vb.SetData<VertexPositionTexture>(0, vertices, 0, 4, VertexPositionTexture.SizeInBytes);

             ib = new IndexBuffer(graphicsDevice, typeof(int), 6, BufferUsage.None);
             ib.SetData<int>(indices, 0, 6);

             graphicsDevice.RenderState.FillMode = FillMode.Solid;

             graphicsDevice.VertexDeclaration = vertexDeclaration;
             graphicsDevice.Textures[0] = texture;
             graphicsDevice.Vertices[0].SetSource(vb, 0, VertexPositionTexture.SizeInBytes);
             graphicsDevice.Indices = ib;

             graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 2);
             //graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, 4, indices, 0, 2);
         }
    }
}
