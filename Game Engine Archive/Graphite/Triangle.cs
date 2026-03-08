using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    class Triangle : DrawableGameComponent
    {

        VertexPositionColor[] vertices;
        VertexBuffer vb;
        VertexDeclaration declaration;
        Shader transformColor;

        public Triangle(Game game) : base(game)
        {
            transformColor = new Shader("transformColor", game.Content);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(5, 0, -10);
            vertices[1].Position = new Vector3(0, 5, -10);
            vertices[2].Position = new Vector3(-5, 0, -10);

            vertices[0].Color = Color.Red;
            vertices[1].Color = Color.White;
            vertices[2].Color = Color.Blue;

            
            declaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            vb = new VertexBuffer(GraphicsDevice, VertexPositionColor.SizeInBytes * 3, BufferUsage.None);
            vb.SetData<VertexPositionColor>(vertices);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Vertices[0].SetSource(vb, 0, VertexPositionColor.SizeInBytes);
            GraphicsDevice.VertexDeclaration = declaration;

            transformColor.SetParameters();

            GraphicsDevice.RenderState.FillMode = FillMode.Solid;
            GraphicsDevice.RenderState.CullMode = CullMode.None;

            transformColor.Effect.Begin();

            foreach (EffectPass pass in transformColor.Effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                pass.End();
            }

            transformColor.Effect.End();
        }
    }
}
