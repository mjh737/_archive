using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class sParticleGravity : sParticleForceGenerator
    {
        //Acceleration due to gravity
        sVector3 gravity;

        public sParticleGravity(sVector3 gravity)
        {
            this.gravity = gravity;
        }

        public override void UpdateForce(sParticle particle, float duration)
        {
            particle.AddForce(gravity * particle.Mass);
        }
    }
}
