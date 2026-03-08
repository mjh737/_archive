using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class xPerlinNoiseSettings
    {
        int seed; public int Seed { get { return seed; } set { seed = value; } }
        float persistence; public float Persistence { get { return persistence; } }
        int octave; public int Octave { get { return octave; } }
        float noiseSize; 

        public xPerlinNoiseSettings()
        {
            this.seed = 0;
            this.persistence = 0.0f;
            this.octave = 0;
            this.noiseSize = 0.0f;
        }

        public xPerlinNoiseSettings(int seed, float persistence, int octave, float noiseSize)
        {
            this.seed = seed;
            this.persistence = persistence;
            this.octave = octave;
            this.noiseSize = noiseSize;
        }
    }
}
