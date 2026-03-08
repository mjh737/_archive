using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class sParticleDrag : sParticleForceGenerator
    {
        float k1; //The Velocity Drag Coefficient
        float k2; //The Velocity Squared Drag Coefficient

        public sParticleDrag(float k1, float k2)
        {
            this.k1 = k1;
            this.k2 = k2;
        }

        public override void UpdateForce(sParticle particle, float duration)
        {
            sVector3 force = particle.Velocity;
            float dragCoefficient = force.Magnitude();
            dragCoefficient = k1 * dragCoefficient + k2 * dragCoefficient * dragCoefficient;

            force.Normalize();
            force *= -dragCoefficient;
            particle.AddForce(force);
        }
    }
}
