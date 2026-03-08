using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Maia.Services
{
    interface ICamera
    {
        Matrix World { get; set; }
        Matrix View { get; set; }
        Matrix Projection { get; set; }

        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }

        void Translate(Vector3 distance);
        void Rotate(Vector3 axis, float angle);
    }
}
