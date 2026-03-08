using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    public class Line : DrawableGameComponent
    {
        VertexPositionColor[] verts;
        VertexDeclaration vertexDeclaration;

        public Line(Game game, Vector3 p1, Vector3 p2) : this(game, p1, p2, Color.Black)
        {
        }

        public Line(Game game, Vector3 p1, Vector3 p2, Color color) : base(game)
        {
            verts = new VertexPositionColor[2];
            verts[0].Position.X = p1.X;
            verts[0].Position.Y = p1.Y;
            verts[0].Position.Z = p1.Z;
            verts[0].Color = color;
            verts[1].Position.X = p2.X;
            verts[1].Position.Y = p2.Y;
            verts[1].Position.Z = p2.Z;
            verts[1].Color = color;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            vertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.VertexDeclaration = vertexDeclaration;
            GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, verts, 0, 1);
        }
    }
}
