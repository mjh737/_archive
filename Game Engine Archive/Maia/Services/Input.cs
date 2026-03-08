using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Maia.Services
{
    public class Input : GameComponent, IInput
    {
        float speed = 5;

        public KeyboardState CurrentKeyboardState;
        public MouseState CurrentMouseState;

        public KeyboardState PreviousKeyboardState;
        public MouseState PreviousMouseState;

        private Point lastMouseLocation;
        private Vector2 mouseMove; public Vector2 MouseMove { get { return mouseMove; } }

        ICamera Camera;

        public Input(Game game) : base(game)
        {
            PreviousKeyboardState = new KeyboardState();
            CurrentKeyboardState = new KeyboardState();
            PreviousMouseState = new MouseState();
            CurrentMouseState = new MouseState();
        }

        public override void Initialize()
        {
            Camera = (ICamera)(Game.Services.GetService(typeof(ICamera)));
        }

        public override void Update(GameTime gameTime)
        {
            if (CurrentKeyboardState.IsKeyDown(Keys.Escape)) Game.Exit();

            if (CurrentKeyboardState.IsKeyDown(Keys.A)) Camera.Translate(new Vector3(-0.01f * speed, 0, 0));
            if (CurrentKeyboardState.IsKeyDown(Keys.D)) Camera.Translate(new Vector3(0.01f * speed, 0, 0));
            if (CurrentKeyboardState.IsKeyDown(Keys.W)) Camera.Translate(new Vector3(0, 0, -0.01f * speed));
            if (CurrentKeyboardState.IsKeyDown(Keys.S)) Camera.Translate(new Vector3(0, 0, 0.01f * speed));
            if (CurrentKeyboardState.IsKeyDown(Keys.Q)) Camera.Translate(new Vector3(0, 0.01f * speed, 0));
            if (CurrentKeyboardState.IsKeyDown(Keys.Z)) Camera.Translate(new Vector3(0, -0.01f * speed, 0));

            Camera.Rotate(Camera.View.Up, MouseMove.X * 0.01f);
            Camera.Rotate(new Vector3(1, 0, 0), MouseMove.Y * 0.01f);

            PreviousKeyboardState = CurrentKeyboardState;
            PreviousMouseState = CurrentMouseState;

            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();

            mouseMove = new Vector2(PreviousMouseState.X - CurrentMouseState.X, PreviousMouseState.Y - CurrentMouseState.Y);
            lastMouseLocation = new Point(CurrentMouseState.X, CurrentMouseState.Y);
        }

        //Helper to check for new key presses
        public bool NewKeyPressed(Keys key)
        {
            return (CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key));
        }
    }
}
