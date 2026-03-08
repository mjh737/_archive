using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    class sParticleSpring : sParticleForceGenerator
    {
        
        sParticle other;        // The other end of the spring
        float springConstant;   // The Spring Constant
        float restLength;       // The rest length of the spring

        public sParticleSpring(sParticle other, float springConstant, float restLength)
        {
            this.other = other;
            this.springConstant = springConstant;
            this.restLength = restLength;
        }

        public override void UpdateForce(sParticle particle, float duration)
        {
            sVector3 force = particle.position;
            force -= other.position;

            //Calculate the magnitude of the force
            float magnitude = force.Magnitude();
            magnitude = Math.Abs(magnitude - restLength);
            magnitude *= springConstant;

            //Calculate the final force and apply it
            force.Normalize();
            force *= -magnitude;
            particle.AddForce(force);
        }

    }
}
