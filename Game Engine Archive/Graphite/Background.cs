using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphite
{
    public class Background : DrawableGameComponent
    {
        private Texture2D texture;
        private string content;

        SpriteBatch spriteBatch;

        Rectangle fullscreen;

        public Background(Game game, string content) : base(game)
        {
            this.content = content;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void  LoadContent()
        {
            fullscreen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            texture = Game.Content.Load<Texture2D>(content) as Texture2D;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();

            spriteBatch.Draw(texture, fullscreen, Color.White);

            spriteBatch.End();
        }
    }
}
