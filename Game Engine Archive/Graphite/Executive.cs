using Graphite.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Graphite
{
    public class Executive : Game
    {
        GraphicsDeviceManager graphics;
        static Camera camera;
        static public Camera Camera { get { return camera; } }
        static Input input;
        static public Input Input { get { return input; } }

        Axes axes;
        Background background;
        Grid grid;
        HUD hud;
        FPSManager fps;
        Triangle triangle;
        TestTerrain2 terrain;
        Origin origin;
        Line line;
        SkyBox sky;
        BasicModel model;

        public Executive(string title)
        {
            this.Window.Title = title;

            Window.ClientSizeChanged += new EventHandler(WindowClientSizeChanged);

            

            background = new Background(this, "background");
            axes = new Axes(this);
            grid = new Grid(this);
            hud = new HUD(this);
            fps = new FPSManager(this);
            triangle = new Triangle(this);
            //terrain = new TestTerrain2(this);
            origin = new Origin(this, Color.Orange);
            line = new Line(this, new Vector3(1000, 1000, 1000), new Vector3(-1000, 1000, -1000), Color.Yellow);
            sky = new SkyBox(this);
            model = new BasicModel(this, "tiger");
        }

        protected override void Initialize()
        {
            base.Initialize();

            camera = new Camera(this);
            input = new Input(this);

            Components.Add(camera);
            Components.Add(input);
            //Components.Add(terrain);
            //Components.Add(background);
            //Components.Add(axes);
            //Components.Add(grid);
            Components.Add(hud);
            Components.Add(fps);
            Components.Add(triangle);
            //Components.Add(origin);
            //Components.Add(line);
            //Components.Add(sky);
            Components.Add(model);

            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";

            
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            graphics.GraphicsDevice.RenderState.CullMode = CullMode.CullClockwiseFace;

            base.Draw(gameTime);
        }

        void WindowClientSizeChanged(object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        }
    }
}
