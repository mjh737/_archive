using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Graphite
{
    public class Origin : DrawableGameComponent
    {
        VertexPositionColor[] point;

        public Origin(Game game, Color color) : base(game)
        {
            point = new VertexPositionColor[1];
            point[0].Position = Vector3.Zero;
            point[0].Color = color;

            //throw new Exception("Doesn't Work Yet!");
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.VertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.PointList, point, 0, 1);
            GraphicsDevice.DrawPrimitives(PrimitiveType.PointList, 0, 1);
        }
    }
}
