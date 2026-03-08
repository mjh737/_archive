using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Maia.Services;

namespace Maia.Components
{
    public class HUD : DrawableGameComponent
    {
        private static int FPS = 0;            // Keeps track of the current framerate.
        private static int frameCount = 0;     // Keeps track of how many frames have passed.   
        private static int elapsedTime = 0;    // Totals the time.

        SpriteBatch sprite;
        SpriteFont font;

        ICamera Camera;

        public HUD(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            
            sprite = new SpriteBatch(GraphicsDevice);

            Camera = (ICamera)Game.Services.GetService(typeof(ICamera));
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("tahoma");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Increase our time
            elapsedTime += gameTime.ElapsedRealTime.Milliseconds;

            // If the time has passed a second
            if (elapsedTime > 1000)
            {
                // Set the framerate
                FPS = frameCount;
                // Reset our fields
                elapsedTime = 0;
                frameCount = 0;
            }

            // Up the framecount
            frameCount++;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RenderState.FillMode = FillMode.Solid;

            sprite.Begin();

            string pos = "Position: " + 
                Camera.Position.X.ToString("F0") + ", " +
                Camera.Position.Y.ToString("F0") + ", " +
                Camera.Position.Z.ToString("F0");

            string rot = "Rotation: " +
                (Camera.Rotation.X * 90).ToString("F0") + ", " +
                (Camera.Rotation.Y * 90).ToString("F0") + ", " +
                (Camera.Rotation.Z * 90).ToString("F0") + ", ";

            string fps = "FPS: " + FPS.ToString();

            sprite.DrawString(
                font,
                pos,
                new Vector2(5, 5),
                Color.Red);

            sprite.DrawString(
                font,
                rot,
                new Vector2(5, 17),
                Color.Red);

            sprite.DrawString(
                font,
                fps,
                new Vector2(5, 29),
                Color.Red);

            sprite.End();

            base.Draw(gameTime);
        }
    }
}