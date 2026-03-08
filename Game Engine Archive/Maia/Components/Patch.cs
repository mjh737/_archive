using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maia.Services;
using Graphite;

namespace Maia.Components
{
    public class Patch : DrawableGameComponent
    {
        ICamera Camera;
        BasicEffect effect;
        HeightMap heightMap;

        VertexPositionColor[] vertices;
        int[] indices;
        VertexDeclaration vertexDeclaration;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        int width = 512;
        int height = 512;

        public Patch(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            Camera = (ICamera)Game.Services.GetService(typeof(ICamera));

            base.Initialize();

            effect = new BasicEffect(GraphicsDevice, null);

            heightMap = new HeightMap(@"C:\Users\Matt\Desktop\Shuttle\Visual Studio 2008\Projects\Xapphire\Pantheon\Maia\Content\heightmap1024.bmp");

            initializeVertices();
            initializeIndices();

            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);
            GraphicsDevice.VertexDeclaration = vertexDeclaration;

            vertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.SizeInBytes * vertices.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vertices);

            indexBuffer = new IndexBuffer(GraphicsDevice, typeof(int), indices.Length, BufferUsage.None);
            indexBuffer.SetData<int>(indices);

            GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionColor.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
        }

        private void initializeVertices()
        {
            vertices = new VertexPositionColor[width * height];

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    vertices[z * height + x] = new VertexPositionColor(new Vector3(x, heightMap.heightData[x, z], z), Color.Red);
                }
            }
        }

        private void initializeIndices()
        {
            indices = new int[3 * 2 * width * height];

            int counter = 0;
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    int lowerLeft = x + y * height;
                    int lowerRight = (x + 1) + y * height;
                    int topLeft = x + (y + 1) * height;
                    int topRight = (x + 1) + (y + 1) * height;

                    indices[counter++] = topLeft;
                    indices[counter++] = lowerRight;
                    indices[counter++] = lowerLeft;

                    indices[counter++] = topLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = lowerRight;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            effect.World = Camera.World;
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;

            effect.Begin();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
                pass.End();
            }

            effect.End();

            base.Draw(gameTime);
        }
    }
}