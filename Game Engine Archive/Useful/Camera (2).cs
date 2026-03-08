using System;
using Microsoft.DirectX;

namespace Engine
{
    public class Camera
    {
        private Vector3 position = new Vector3();
        private Vector3 target = new Vector3();
        private Vector3 upVector = new Vector3(0, 1, 0);

        private int screenWidth;
        private int screenHeight;

        private float nearClip = 1.0f;
        private float farClip = 1000.0f;
        private float fov = (float)Math.PI / 4.0f;

        private float hRotation = -(float)Math.PI / 2.0f;
        private float vRotation = 0.0f;
        private float radius = 10.0f;

        public Vector3 Position { get { return position; } }
        public Vector4 Position4 { get { return new Vector4(position.X, position.Y, position.Z, 0); } }

        public Matrix World { get { return Matrix.Identity; } }
        public Matrix View { get { return Matrix.LookAtLH(position, target, upVector); } }
        public Matrix Projection
        {
            get
            {
                return Matrix.PerspectiveFovLH(
                    fov,
                    (float)screenWidth / (float)screenHeight,
                    nearClip, farClip
                );
            }
        }

        public Camera(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;

            UpdatePosition();
        }

        public void Rotate(float h, float v)
        {
            hRotation += h;
            vRotation += v;

            UpdatePosition();
        }

        public void Zoom(float dist)
        {
            radius += dist;
            if (radius < .01f) radius = .01f;

            UpdatePosition();
        }

        public void SetTarget(Vector3 newTarget)
        {
            target = newTarget;
        }

        public void SetRadius(float newRadius)
        {
            radius = newRadius;
        }

        public void UpdatePosition()
        {
            // (radius * Math.Cos(vRotation)) is the temporary radius after the y component shift
            position.X = (float)(radius * Math.Cos(vRotation) * Math.Cos(hRotation));
            position.Y = (float)(radius * Math.Sin(vRotation));
            position.Z = (float)(radius * Math.Cos(vRotation) * Math.Sin(hRotation));

            // Keep all rotations between 0 and 2PI
            hRotation = hRotation > (float)Math.PI * 2 ? hRotation - (float)Math.PI * 2 : hRotation;
            hRotation = hRotation < 0 ? hRotation + (float)Math.PI * 2 : hRotation;

            vRotation = vRotation > (float)Math.PI * 2 ? vRotation - (float)Math.PI * 2 : vRotation;
            vRotation = vRotation < 0 ? vRotation + (float)Math.PI * 2 : vRotation;

            // Switch up-vector based on vertical rotation
            upVector = vRotation > Math.PI / 2 && vRotation < Math.PI / 2 * 3 ?
                new Vector3(0, -1, 0) : new Vector3(0, 1, 0);

            // Translate these coordinates by the target objects spacial location
            position += target;
        }
    }
}