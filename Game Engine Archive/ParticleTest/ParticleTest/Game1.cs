using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace ParticleTest
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        List<Particle> particles;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Random r = new Random();

        public static int numParticles = 10000;

        public static Vector2 origin;
        public static float speed = 10;
        public static float energyDisipation = 1f;
        public static float wallFriction = 0.9f;
        public static Vector2 gravity = new Vector2(0, 0.1f);
        public static float airResistance = 0.999f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            particles = new List<Particle>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            origin = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            for (int i = 0; i < numParticles; i++)
            {
                AddParticle();
            }
        }

        protected override void LoadContent()
        {
            texture = Content.Load<Texture2D>("particle");
            
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (Particle particle in particles)
            {
                //Check for wall friction
                //if (particle.Position.Y == 700 && particle.Heading.Y < 0.1f) particle.Heading = new Vector2(particle.Heading.X * wallFriction, particle.Heading.Y);

                //apply air resistance
                //particle.Heading *= airResistance;

                //apply gravity
                //particle.Heading += gravity;

                particle.Position += particle.Heading;

                //Bounds Checking
                if (particle.Position.X < 0 || particle.Position.X > 1280)
                {
                    particle.Heading = Vector2.Reflect(particle.Heading, new Vector2(1,0));
                }

                if (particle.Position.Y < 0 || particle.Position.Y > 700)
                {
                    particle.Heading = Vector2.Reflect(particle.Heading, new Vector2(0, 1));
                }

                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (Particle particle in particles)
            {
                spriteBatch.Draw(texture, particle.Position, particle.Color);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AddParticle()
        {
            Particle p = new Particle();

            byte[] bytes = new byte[3];
            r.NextBytes(bytes);
            p.Color = new Color(bytes[0], bytes[1], bytes[2]);
            p.Position = origin;
            p.Heading = new Vector2(((float)r.NextDouble() * speed * 2f) - speed, ((float)r.NextDouble() * speed * 2) - speed);
            //p.Speed = speed * (float)r.NextDouble();
            particles.Add(p);
        }
    }
}
