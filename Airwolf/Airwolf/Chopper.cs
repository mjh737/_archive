using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airwolf
{
    class Chopper
    {
        Vector2 position; public Vector2 Position { get { return position; } set {position = value; } }
        Texture2D tile; public Texture2D Tile { get { return tile; } set { tile = value; } }
        int tileWidth = 20; public int TileWidth { get { return tileWidth; } }
        int tileHeight = 20; public int TileHeight { get { return tileHeight; } }
        public float Speed = 0.1f;

        public Chopper()
        {
            position = Vector2.Zero;
        }
    }
}
