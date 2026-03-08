using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Maia.Services;
using Maia.Components;

namespace Maia
{
    public class Executive : Game
    {
        public GraphicsDeviceManager Graphics { get; set; }

        //Services
        Camera Camera;
        Input input;

        //Game Components
        Axes axes;
        HUD hud;
        Patch patch;
        Triangle triangle;

        public Executive()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;

            Graphics.ApplyChanges();

            Window.Title = "Maia Terrain Engine";

            Camera = new Camera(this);
            input = new Input(this);

            axes = new Axes(this);
            hud = new HUD(this);
            patch = new Patch(this);
            triangle = new Triangle(this);

            Services.AddService(typeof(ICamera), Camera);
            Services.AddService(typeof(IInput), input);

            Components.Add(Camera);
            Components.Add(input);
            Components.Add(axes);
            Components.Add(triangle);
            Components.Add(patch);
            Components.Add(hud);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
