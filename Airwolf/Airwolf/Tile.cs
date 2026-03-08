using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Airwolf
{
    class Tile
    {
        Texture2D image; public Texture2D Image { get { return image; } set { image = value; } }
        bool isPermeable; public bool IsPermeable { get { return isPermeable; } set { isPermeable = value; } }

        public Tile(bool isPermeable, string assetName, ContentManager Content)
        {
            image = Content.Load<Texture2D>(Content.RootDirectory + @"\\" +assetName);
            isPermeable = false;
        }
    }
}
