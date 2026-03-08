using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace StarField
{
    public class Declination
    {
        int sign;
        int degrees;
        float minutes;

        float radians; public float Radians { get { return radians; } }


        public Declination(int sign, int degrees, float minutes)
        {
            this.sign = Math.Sign(sign);
            this.degrees = degrees % 90;
            this.minutes = minutes % 60;

            radians = ConvertToRadians();
        }

        private float ConvertToRadians()
        {
            float decInRadians = (float)(sign * (((degrees + (minutes / 60)) / 90) * Math.PI / 2));

            return decInRadians;
        }
    }
}
