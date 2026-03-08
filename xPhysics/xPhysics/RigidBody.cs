using Microsoft.Xna.Framework;

namespace xPhysics
{
    public class RigidBody
    {
        const float damping = 1.0f;

        float mass;
        float inverseMass;

        Vector3 forceAccumulator;

        Vector3 position;
        Vector3 velocity;
        Vector3 acceleration;

        public float Mass { get { return mass; } set { mass = value; inverseMass = 1 / mass; } }
        public Vector3 Position { get { return position; } set { position = value; } }
        public Vector3 Velocity { get { return velocity; } set { velocity = value; } }
        public Vector3 Acceleration { get { return acceleration; } set { acceleration = value; } }


        public RigidBody()
        {
            position = Vector3.Zero;
            velocity = Vector3.Zero;
            acceleration = Vector3.Zero;
            ClearAccumulator();
        }

        public void UpdatePosition(float seconds)
        {
            //Update position
            position += velocity * seconds;

            //Update velocity
            velocity += forceAccumulator * inverseMass * seconds;

            //Impose Damping
            velocity *= damping;

        }

        public void AddForce(Vector3 force)
        {
            forceAccumulator += force;
        }

        public void ClearAccumulator()
        {
            forceAccumulator = Vector3.Zero;
        }
    }
}
