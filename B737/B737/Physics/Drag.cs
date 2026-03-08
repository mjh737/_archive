namespace B737.Physics
{
    using System.Numerics;

    public class Drag : IForce
    {
        private readonly double dragCoefficient;
        private readonly double referenceArea;

        public Drag(double cd)
        {
            this.dragCoefficient = cd;
        }

        public Vector3 CalculateForce(AircraftState state)
        {
            // Drag magnitude: D = 0.5 * rho * V^2 * S * Cd
            double drag = 0.5 * Atmosphere.AmbientPressurePa * state.VelocitySquared * referenceArea * dragCoefficient;

            // Direction: opposite to velocity vector
            if (state.Velocity == Vector3.Zero)
                return Vector3.Zero;

            Vector3 dragDirection = -Vector3.Normalize(state.Velocity);

            // Cast once to float for Vector3
            return dragDirection * (float)drag;
        }
    }
}