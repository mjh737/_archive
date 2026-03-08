using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Graphite
{
    public class TexturedQuad : DrawableGameComponent
    {
        private string asset;
        private Texture2D texture;
        private VertexBuffer vb;
        private IndexBuffer ib;
        VertexPositionTexture[] vertices;
        int[] indices = { 0, 1, 2, 1, 3, 2 };
        Shader shader;
        Vector3 position;
        float scaling = 1;
        public float Scaling { get { return scaling; } set { scaling = value; } }
        public Vector3 Position { get { return position; } set { position = value; } }

        public TexturedQuad(Game game, string texture) : base(game)
        {
            asset = texture;
            shader = new Shader(@"TransformTexture", game.Content);
        }

        public override void Initialize()
        {
            vertices = new VertexPositionTexture[4];

            vertices[0] = new VertexPositionTexture(new Vector3(position.X-(0.5f * scaling), position.Y+(0.5f * scaling), position.Z), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(position.X+(0.5f * scaling), position.Y+(0.5f * scaling), position.Z), new Vector2(1, 0));
            vertices[2] = new VertexPositionTexture(new Vector3(position.X-(0.5f * scaling), position.Y-(0.5f * scaling), position.Z), new Vector2(0, 1));
            vertices[3] = new VertexPositionTexture(new Vector3(position.X + (0.5f * scaling), position.Y-(0.5f * scaling), position.Z), new Vector2(1, 1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(asset) as Texture2D;
        }

        public override void Update(GameTime gameTime)
        {
            shader.SetParameters();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            vb = new VertexBuffer(GraphicsDevice, VertexPositionTexture.SizeInBytes * 4, BufferUsage.None);
            vb.SetData<VertexPositionTexture>(0, vertices, 0, 4, VertexPositionTexture.SizeInBytes);

            ib = new IndexBuffer(GraphicsDevice, typeof(int), 6, BufferUsage.None);
            ib.SetData<int>(indices, 0, 6);

            using (VertexDeclaration declaration = new VertexDeclaration(GraphicsDevice, VertexPositionTexture.VertexElements))
            {
                GraphicsDevice.VertexDeclaration = declaration;
                GraphicsDevice.Textures[0] = texture;
                GraphicsDevice.Vertices[0].SetSource(vb, 0, VertexPositionTexture.SizeInBytes);
                GraphicsDevice.Indices = ib;

                shader.Effect.Begin();
                foreach (EffectPass pass in shader.Effect.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, 4, indices, 0, 2);
                    pass.End();
                }
                shader.Effect.End();
            }
        }

    }
}
