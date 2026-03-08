using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Graphite
{
    public class TestTerrain2 : DrawableGameComponent
    {
        VertexPositionNormalTexture[] vertices;
        int[] indices;
        VertexDeclaration vertexDeclaration;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        private int WIDTH;
        private int HEIGHT;
        private int[,] heightData;
        Shader textureShader;
        Texture2D texture;
        
        
        public TestTerrain2(Game game) : base(game)
        {
            LoadHeightData();

            InitializeVertices();
            InitializeIndices();

            textureShader = new Shader("TextureShader", game.Content);
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionNormalTexture[WIDTH * HEIGHT];

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    vertices[x + y * WIDTH].Position = new Vector3(x, heightData[x, y] / 3 - 6, y);
                    vertices[x + y * WIDTH].Normal = Vector3.Up;
                    //vertices[x + y * WIDTH].TextureCoordinate = new Vector2(x / WIDTH, y / HEIGHT);

                    
                }
            }

            ComputeNormals();
        }

        private void ComputeNormals()
        {
            for (int z = 1; z < HEIGHT-1; z++)
            {
                for (int x = 1; x < WIDTH-1; x++)
                {
                    Vector3 X = Vector3.Subtract(
                    vertices[z * HEIGHT + x + 1].Position,
                    vertices[z * HEIGHT + x - 1].Position);

                    Vector3 Z = Vector3.Subtract(
                    vertices[(z + 1) * HEIGHT + x].Position,
                    vertices[(z - 1) * HEIGHT + x].Position);

                    Vector3 normal = Vector3.Cross(Z, X);
                    normal.Normalize();
                    vertices[(z * HEIGHT) + x].Normal = normal;
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

            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionNormalTexture.VertexElements);

            vertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionNormalTexture.SizeInBytes * WIDTH * HEIGHT, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);

            indexBuffer = new IndexBuffer(GraphicsDevice, typeof(int), indices.Length, BufferUsage.None);
            indexBuffer.SetData<int>(indices);

            
        }

        protected override void LoadContent()
        {
            LoadHeightData();

            texture = Game.Content.Load<Texture2D>(@"landfill");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            textureShader.SetParameters();

            textureShader.Effect.Parameters["myTexture"].SetValue(texture);
            textureShader.Effect.CurrentTechnique = textureShader.Effect.Techniques["TransformTexture"];

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.VertexDeclaration = vertexDeclaration;
            GraphicsDevice.Vertices[0].SetSource(vertexBuffer, 0, VertexPositionNormalTexture.SizeInBytes);
            GraphicsDevice.Indices = indexBuffer;
            GraphicsDevice.Textures[0] = texture;

            GraphicsDevice.RenderState.FillMode = FillMode.Solid;

            textureShader.Effect.Begin();
            foreach (EffectPass pass in textureShader.Effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, WIDTH * HEIGHT / 2);
                pass.End();
            }
            textureShader.Effect.End();
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
