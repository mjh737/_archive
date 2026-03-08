using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class sParticle
    {
        public sVector3 position; //Linear Position
        
        public sVector3 acceleration; //Acceleration

        private sVector3 velocity; //Linear Velocity
        public sVector3 Velocity { set { velocity = value; } get { return velocity; } }
        
        private sVector3 forceAccum;
        private float mass;
        public float Mass
        {
            set
            {
                mass = value;
                inverseMass = 1 / value;
            }
            get
            {
                return mass;
            }
        }
        private float inverseMass; //Easier to work with 1/m
        public float InverseMass
        {
            set
            {
                inverseMass = value;
                mass = 1 / value;
            }
            get
            {
                return inverseMass;
            }
        }

        private float damping; //EG drag (to compensate for CPU inaccuracies)
        public float Damping
        {
            set
            {
                if (value >= 1) value = 0.995f; // 1 equals no damping effect
                if (value <= 0) value = 0; // 0 equals no movement without force
                damping = value; }
            get { return damping; }
        }

        public void Integrate(float duration)
        {
            if (duration <= 0.0) return;

            //Update linear position
            position.AddScaledVector(velocity, duration);

            //Calculate Acceleration
            sVector3 acc = acceleration;
            acc.AddScaledVector(forceAccum, inverseMass);

            //Update linear velocity
            velocity.AddScaledVector(acc, duration);

            //Impose drag
            velocity = velocity * (float)Math.Pow(damping, duration);

            //Clear the forces
            ClearAccumulator();
        }

        private void ClearAccumulator()
        {
            forceAccum.Clear();
        }

        public void AddForce(sVector3 force)
        {
            forceAccum += force;
        }

    }
}
