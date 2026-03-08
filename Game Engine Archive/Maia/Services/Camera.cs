using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maia.Services
{
    public class Camera : GameComponent, ICamera
    {
        int width;
        int height;

        public float FOV { get; set; }

        private float near = 0.1f;
        public float Near { get { return near; } set { near = value; } }

        private float far = 3500.0f;
        public float Far { get { return far; } set { far = value; } }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Matrix World { get; set; }
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }

        IInput input;

        public Camera(Game game) : base(game)
        {
            Position = new Vector3(0, 5, -10);
            Rotation = Quaternion.Identity;

            FOV = 1;
        }

        public override void Initialize()
        {
            width = Game.GraphicsDevice.Viewport.Width;
            height = Game.GraphicsDevice.Viewport.Height;

            input = (IInput)(Game.Services.GetService(typeof(IInput)));
        }

        public override void Update(GameTime gameTime)
        {
            World = Matrix.Identity;
            View = Matrix.Invert(Matrix.CreateFromQuaternion(Rotation)
                * Matrix.CreateTranslation(Position));
            Projection = Matrix.CreatePerspectiveFieldOfView(FOV, width/height , near, far);

            Rotate(View.Up, input.MouseMove.X * 0.01f);
            Rotate(Vector3.Right, input.MouseMove.Y * 0.01f);
        }

        public void Rotate(Vector3 axis, float angle)
        {
            axis = Vector3.Transform(axis, Matrix.CreateFromQuaternion(Rotation));
            Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(axis, angle) * Rotation);
        }

        public void Translate(Vector3 distance)
        {
            Position += Vector3.Transform(distance, Matrix.CreateFromQuaternion(Rotation));
        }
    }
}
