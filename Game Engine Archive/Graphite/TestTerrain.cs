using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Graphite
{
    public class TestTerrain : DrawableGameComponent
    {
        VertexPositionColor[] vertices;
        int[] indices;
        VertexDeclaration vertexDeclaration;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        private int WIDTH;
        private int HEIGHT;
        private int[,] heightData;
        Shader transformColor;
        
        
        public TestTerrain(Game game) : base(game)
        {
            LoadHeightData();

            InitializeVertices();
            InitializeIndices();

            transformColor = new Shader("TransformColor", game.Content);
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionColor[WIDTH * HEIGHT];

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    vertices[x + y * WIDTH].Position = new Vector3(x, heightData[x, y] / 3 - 6, y);
                    vertices[x + y * WIDTH].Color = Color.AntiqueWhite;
                }
            }
        }

        private void InitializeIndices()
        {
            indices = new int[3 * 2 * WIDTH * HEIGHT];

            int counter = 0;
            for (int y = 0; y < HEIGHT - 1; y++)
            {
                for (int x = 0; x < WIDTH - 1; x++)
                {
                    int lowerLeft = x + y * HEIGHT;
                    int lowerRight = (x + 1) + y * HEIGHT;
                    int topLeft = x + (y + 1) * HEIGHT;
                    int topRight = (x + 1) + (y + 1) * HEIGHT;

                    indices[counter++] = topLeft;
                    indices[counter++] = lowerRight;
                    indices[counter++] = lowerLeft;

                    indices[counter++] = topLeft;
                    indices[counter++] = topRight;
                    indices[counter++] = lowerRight;
                }
            }
        }

        public override void Initialize()
        {
            

            base.Initialize();

            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            vertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionColor.SizeInBytes * WIDTH * HEIGHT, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vertices);

            indexBuffer = new IndexBuffer(GraphicsDevice, typeof(int), indices.Length, BufferUsage.None);
            indexBuffer.SetData<int>(indices);

            
        }

        protected override void LoadContent()
        {
            LoadHeightData();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            transformColor.SetParameters();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.VertexDeclaration = vertexDeclaration;
            GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionColor.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;

            GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;

            transformColor.Effect.Begin();
            foreach (EffectPass pass in transformColor.Effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, WIDTH * HEIGHT / 2);
                pass.End();
            }
            transformColor.Effect.End();
        }



        private void LoadHeightData()
        {
            HeightMap heightmap = new HeightMap(@"..\..\..\Content\heightmap.bmp");
            heightData = heightmap.heightData;
            WIDTH = heightmap.WIDTH;
            HEIGHT = heightmap.HEIGHT;
        }
    }
}
