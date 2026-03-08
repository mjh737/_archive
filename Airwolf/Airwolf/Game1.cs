using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Airwolf
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level level = new Level();
        Tile wallTile;
        Tile emptyTile;

        Chopper airwolf = new Chopper();

        int centreScreenX, centreScreenY;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            wallTile  = new Tile(true, "BlueTile", Content);
            emptyTile = new Tile(false, "EmptyTile", Content);
        }

        protected override void Initialize()
        {
            centreScreenX = GraphicsDevice.Viewport.Width / 2;
            centreScreenY = GraphicsDevice.Viewport.Height / 2;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            airwolf.Tile = Content.Load<Texture2D>("Airwolf");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();

            Vector2 movement = Vector2.Zero;

            if (keys.IsKeyDown(Keys.Right)) movement.X++;
            if (keys.IsKeyDown(Keys.Left)) movement.X--;
            if (keys.IsKeyDown(Keys.Up)) movement.Y--;
            if (keys.IsKeyDown(Keys.Down)) movement.Y++;

            Vector2 previousPosition = airwolf.Position;
            airwolf.Position += movement * airwolf.Speed;



            if (false) 
            {
                airwolf.Position = previousPosition; // If there is a collision don't enter the tile
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float a = airwolf.Position.X;
            float b = airwolf.Position.Y;

            spriteBatch.Begin();

            
            // Draw Tiles
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (level.Map[x, y] == (int)Tiles.Wall) spriteBatch.Draw(wallTile.Image, new Rectangle((int)((x - a )* level.tileWidth), (int)((y - b) * level.tileHeight), level.tileWidth, level.tileHeight), Color.White);
                    if (level.Map[x, y] == (int)Tiles.Empty) spriteBatch.Draw(emptyTile.Image, new Rectangle((int)((x - a) * level.tileWidth), (int)((y - b) * level.tileHeight), level.tileWidth, level.tileHeight), Color.White);
                }
            }

            //Draw Airwolf
            spriteBatch.Draw(airwolf.Tile, new Rectangle(centreScreenX, centreScreenY, airwolf.TileWidth, airwolf.TileHeight), Color.White );

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
