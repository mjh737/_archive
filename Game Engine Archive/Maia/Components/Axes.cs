using Maia.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maia.Components
{
    public class Axes : DrawableGameComponent
    {
        ICamera Camera;
        BasicEffect effect;

        VertexPositionColor[] verts;

        public Axes(Game game) : base(game)
        {
            Color color = Color.Red;

            verts = new VertexPositionColor[6];
            verts[0].Position = new Vector3(-2000, 0, 0);
            verts[0].Color = color;
            verts[1].Position = new Vector3(2000, 0, 0);
            verts[1].Color = color;
            verts[2].Position = new Vector3(0, -2000, 0);
            verts[2].Color = color;
            verts[3].Position = new Vector3(0, 2000, 0);
            verts[3].Color = color;
            verts[4].Position = new Vector3(0, 0, -2000);
            verts[4].Color = color;
            verts[5].Position = new Vector3(0, 0, 2000);
            verts[5].Color = color;
        }

        public override void Initialize()
        {
            Camera = (ICamera)Game.Services.GetService(typeof(ICamera));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            effect = new BasicEffect(GraphicsDevice, null);

            effect.World = Camera.World;
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;
            //effect.EnableDefaultLighting();
            effect.LightingEnabled = false;

            VertexDeclaration dec = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);
            GraphicsDevice.VertexDeclaration = dec;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.FillMode = FillMode.WireFrame;

            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, verts, 0, 3);
                pass.End();
            }
            effect.End();
        }
    }
}
