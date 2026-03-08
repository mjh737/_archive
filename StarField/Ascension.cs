using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace StarField
{
    public class Ascension
    {
        int hours;
        float minutes;

        public float radians; public float Radians { get { return radians; } }

        public Ascension(int hours, float minutes)
        {
            this.hours = hours % 24;
            this.minutes = minutes % 60;

            radians = ConvertToRadians();
        }

        private float ConvertToRadians()
        {
            float ascInRadians = (float)(((hours + (minutes / 60)) / 24) * (Math.PI * 2));

            return ascInRadians;
        }
    }
}
