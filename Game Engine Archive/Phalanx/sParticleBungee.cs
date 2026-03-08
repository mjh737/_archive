using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    class sParticleBungee : sParticleForceGenerator
    {

        sParticle other;        // The other end of the spring
        float springConstant;   // The Spring Constant
        float restLength;       // The rest length of the spring

        public sParticleBungee(sParticle other, float springConstant, float restLength)
        {
            this.other = other;
            this.springConstant = springConstant;
            this.restLength = restLength;
        }

        public override void UpdateForce(sParticle particle, float duration)
        {
            sVector3 force = particle.position;
            force -= other.position;

            
            float magnitude = force.Magnitude();

            //Check if the bungee is compressed
            if(magnitude <= restLength) return;

            //Calculate the magnitude of the force
            magnitude = springConstant * (restLength - magnitude);

            //Calculate the final force and apply it
            force.Normalize();
            force *= -magnitude;
            particle.AddForce(force);
        }

    }
}
