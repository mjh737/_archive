using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Graphite
{
    /// <summary>
    /// Holds a Dictionary of Particles and their Force Generators
    /// </summary>
    class sParticleForceRegistry
    {
        Dictionary<sParticle, sParticleForceGenerator> registrations;

        public sParticleForceRegistry()
        {
            registrations = new Dictionary<sParticle,sParticleForceGenerator>();
        }

        public void Add(sParticle particle, sParticleForceGenerator fg)
        {
            registrations.Add(particle, fg);
        }

        public void Remove(sParticle particle)
        {
            registrations.Remove(particle);
        }

        public void Clear()
        {
            registrations.Clear();
        }

        public void UpdateForces(float duration)
        {
            foreach(KeyValuePair<sParticle, sParticleForceGenerator> kvp in registrations)
            {
                kvp.Value.UpdateForce(kvp.Key, duration); 
            }
        }
    }
}
