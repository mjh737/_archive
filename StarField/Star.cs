using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarField
{
    public class Star
    {
        string name;
        Ascension asc;
        Declination dec;
        double distance;
        Vector3 position; public Vector3 Position { get { return position; } }
        Vector3 refVector;
        Color color; public Color Color { get { return color; } set { color = value; } }
        float magnitude; public float Magnitude { get { return magnitude; } }
        
        public Star(string name, Ascension ra, Declination dec, Color color, float magnitude)
        {
             refVector = new Vector3(0, 0, 1);

            this.name = name;
            this.asc = ra;
            this.dec = dec;
            this.distance = 100;
            this.color = color;
            this.magnitude = magnitude;

            position = CalculatePosition();
        }

        private Vector3 CalculatePosition()
        {
            Quaternion rot = Quaternion.Identity;

            rot = Quaternion.CreateFromYawPitchRoll(asc.Radians, -dec.Radians, 0.0f);

            position = Vector3.Transform(refVector, rot);

            return new Vector3((float)(position.X * distance), (float)(position.Y * distance), (float)(position.Z * distance));
        }


    }
}
