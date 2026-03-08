using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    public class TestBackgroundColor : xObject, IRenderable
    {
        public void Render(GraphicsDevice device)
        {
            device.Clear(Color.OliveDrab); ;
        }
    }
}
