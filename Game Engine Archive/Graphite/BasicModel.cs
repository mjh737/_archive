using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Graphite
{
    public class BasicModel : DrawableGameComponent
    {
        private string asset;
        private Vector3 ambientLightColor = new Vector3(0.25f);
        private Vector3 materialDiffuseColor = new Vector3(0.50f);
        private float specularPower = 32;
        private Model model;
        Shader shader;

        public BasicModel(Game game, string asset) : base(game)
        {
            this.asset = asset;
            shader = new Shader("TextureShader", game.Content);
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>(asset);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            shader.SetParameters();

            shader.Effect.Parameters["AmbientLightColor"].SetValue(new Vector4(ambientLightColor, 1.0f));
            shader.Effect.Parameters["LightDiffuseColor"].SetValue(new Vector4(materialDiffuseColor, 1.0f));
            //shader.Effect.Parameters["SpecularPower"].SetValue(specularPower);
            //shader.Effect.Parameters["EyePosition"].SetValue(Game1.Camera.Position);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                GraphicsDevice.Indices = mesh.IndexBuffer;

                shader.Effect.Begin();

                foreach (EffectPass pass in shader.Effect.CurrentTechnique.Passes)
                {
                    pass.Begin();

                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        GraphicsDevice.VertexDeclaration = part.VertexDeclaration;
                        GraphicsDevice.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);
                        GraphicsDevice.Textures[0] = ((BasicEffect)part.Effect).Texture;
                        GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                    }

                    pass.End();
                }

                shader.Effect.End();
            }
        }
    }
}
