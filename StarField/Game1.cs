using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace StarField
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Effect effect;

        SpriteBatch sprite;
        SpriteFont font;

        float yaw = 0;
        float pitch = 0;

        float x = 0.0f;

        Star[] stars;
        int numStars = 0;

        VertexPositionColor[] vertices;
        VertexBuffer vb;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            StarParse parser = new StarParse(@"C:\Users\Matt\Desktop\stars.dat");

            numStars = parser.NumStars;

            stars = new Star[numStars];

            stars = parser.Stars;

            SetUpXNADevice();
            SetUpVertices();
            SetUpCamera();

            base.Initialize();
        }

        private void SetUpXNADevice()
        {
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Star Field";

            CompiledEffect compiledEffect = Effect.CompileEffectFromFile(@"content\effects.fx", null, null, CompilerOptions.None, TargetPlatform.Windows);
            effect = new Effect(graphics.GraphicsDevice, compiledEffect.GetEffectCode(), CompilerOptions.None, null);
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("SpriteFont1");
            sprite = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape)) this.Exit();

            if (keys.IsKeyDown(Keys.Left)) yaw -= 0.01f;
            if (keys.IsKeyDown(Keys.Right)) yaw += 0.01f;

            if (yaw < 0) yaw += (float)(2 * Math.PI);
            if (yaw > (2 * Math.PI)) yaw -= (float)(2 * Math.PI);

            if (keys.IsKeyDown(Keys.Up)) pitch -= 0.01f;
            if (keys.IsKeyDown(Keys.Down)) pitch += 0.01f;

            if (pitch > 180) pitch = -180;
            if (pitch < -180) pitch = 180;

            Matrix worldMatrix = Matrix.CreateRotationY(yaw) * Matrix.CreateRotationX(pitch);
            effect.Parameters["xWorld"].SetValue(worldMatrix);

            if (keys.IsKeyDown(Keys.Q))
            {
                x += 0.1f;
                SetUpVertices();
            }

            if (keys.IsKeyDown(Keys.A))
            {
                x -= 0.1f;
                SetUpVertices();
            }

            if (x < 0) x = 25;
            if (x > 25) x = 0;

            base.Update(gameTime);
        }
        

        private void SetUpVertices()
        {
            vertices = new VertexPositionColor[numStars];

            for (int n = 0; n < numStars; n++)
            {
                vertices[n].Position = stars[n].Position;

                float col = (stars[n].Magnitude-3);
                if (col < 0) col = 0;
                col *= 50;

                vertices[n].Color = new Color(new Vector3(col, col, col));

            }

            vb = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), numStars, BufferUsage.None);
            vb.SetData(vertices);
        }

        private void SetUpCamera()
        {
            //Matrix rotation = Matrix.CreateFromAxisAngle(Vector3.Up, yaw);



            Matrix viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0,0,1), new Vector3(0, 1, 0));
            Matrix projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, this.Window.ClientBounds.Width / this.Window.ClientBounds.Height, 1.0f, 1000.0f);

            effect.Parameters["xView"].SetValue(viewMatrix);
            effect.Parameters["xProjection"].SetValue(projectionMatrix);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.VertexDeclaration = new VertexDeclaration(GraphicsDevice, VertexPositionColor.VertexElements);

            GraphicsDevice.Vertices[0].SetSource(vb, 0, VertexPositionColor.SizeInBytes);;

            effect.CurrentTechnique = effect.Techniques["Colored"];

            effect.Begin();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();

                    GraphicsDevice.DrawPrimitives(PrimitiveType.PointList, 0, numStars);
                    sprite.Begin();
                    sprite.DrawString(font, "Yaw: " + MathHelper.ToDegrees(yaw) + ", Pitch: " + MathHelper.ToDegrees(pitch) + ", Haze: " + x, new Vector2(10, 10), Color.White);
                    sprite.End();

                pass.End();
            }

            effect.End();

            base.Draw(gameTime);
        }
    }
}
