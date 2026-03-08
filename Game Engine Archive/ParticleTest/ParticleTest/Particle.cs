using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleTest
{
    public class Particle
    {
        Vector2 position;
        public Vector2 Position { 
            get { return position; }
            set { position = value; }
        }

        Color color;
        public Color Color { 
            get { return color; }
            set { color = value; }
        }

        Vector2 heading;
        public Vector2 Heading { 
            get { return heading; }
            set { heading = value; }
        }

        float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

    }
}
