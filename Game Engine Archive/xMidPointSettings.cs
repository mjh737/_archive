using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class xMidPointSettings
    {
        float rough; public float Rough { get { return rough; } set { rough = value; } }

        public xMidPointSettings()
        {
            rough = 0.0f;
        }

        public xMidPointSettings(float rough)
        {
            this.rough = rough;
        }
    }
}
