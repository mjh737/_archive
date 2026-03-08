using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    class sParticleAnchoredSpring : sParticleForceGenerator
    {
        
        sVector3 anchor;        // The anchored end of the spring
        float springConstant;   // The Spring Constant
        float restLength;       // The rest length of the spring

        public sParticleAnchoredSpring(sVector3 anchor, float springConstant, float restLength)
        {
            this.anchor = anchor;
            this.springConstant = springConstant;
            this.restLength = restLength;
        }

        public override void UpdateForce(sParticle particle, float duration)
        {
            sVector3 force = particle.position;
            force -= anchor;

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
