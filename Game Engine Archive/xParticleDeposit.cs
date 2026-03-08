using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class xParticleDepositSettings
    {
        float caldera; public float Caldera { get { return caldera; } }
        int jumps; public int Jumps { get { return jumps; } }
        int peakWalk; public int PeakWalk { get { return peakWalk; } }
        int minParticlesPerJump; public int MinParticlesPerJump { get { return minParticlesPerJump; } }
        int maxParticlesPerJump; public int MaxParticlesPerJump { get { return maxParticlesPerJump; } }

        public xParticleDepositSettings()
        {
            caldera = 0.0f;

            jumps = 0;
            peakWalk = 0;

            minParticlesPerJump = 0;
            maxParticlesPerJump = 0;
        }

        public xParticleDepositSettings(float caldera, int jumps, int peakWalk, int minParticlesPerJump, int maxParticlesPerJump)
        {
            this.caldera = caldera;

            this.jumps = jumps;
            this.peakWalk = peakWalk;

            this.minParticlesPerJump = minParticlesPerJump;
            this.maxParticlesPerJump = maxParticlesPerJump;
        }
    }
}
