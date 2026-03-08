using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maia.Services;

namespace Maia.Components
{
    class Triangle : DrawableGameComponent
    {
        ICamera Camera;

        VertexPositionColor[] vertices;
        VertexBuffer vb;
        VertexDeclaration declaration;
        BasicEffect effect;

        public Triangle(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            Camera = (ICamera)Game.Services.GetService(typeof(ICamera));

            base.Initialize();

            effect = new BasicEffect(GraphicsDevice, null);
        }

        protected override void LoadContent()
        {
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(5, 0, -10);
            vertices[1].Position = new Vector3(0, 5, -10);
            vertices[2].Position = new Vector3(-5, 0, -10);

            vertices[0].Color = Color.White;
            vertices[1].Color = Color.Red;
            vertices[2].Color = Color.White;

            
            declaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            GraphicsDevice.Vertices[0].SetSource(vb, 0, VertexPositionColor.SizeInBytes);
            GraphicsDevice.VertexDeclaration = declaration;

            vb = new VertexBuffer(GraphicsDevice, VertexPositionColor.SizeInBytes * 3, BufferUsage.None);
            vb.SetData<VertexPositionColor>(vertices);
        }

        public override void Update(GameTime gameTime)
        {
            effect.World = Camera.World;
            effect.Projection = Camera.Projection;
            effect.View = Camera.View;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.FillMode = FillMode.Solid;
            GraphicsDevice.RenderState.CullMode = CullMode.None;

            effect.Begin();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

                pass.End();
            }

            effect.End();
        }
    }
}
