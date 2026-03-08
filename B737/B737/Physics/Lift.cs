namespace B737.Physics
{
    using B737.Constants;
    using System.Numerics;

    public class Lift : IForce
    {
        private readonly double liftCoefficient;

        public Lift(double cl)
        {
            this.liftCoefficient = cl;
        }

        public Vector3 CalculateForce(AircraftState state)
        {
            // Use double precision for physics
            double lift = 0.5 * Atmosphere.AmbientPressurePa * state.VelocitySquared * Aerodynamics.WingAreaM2 * liftCoefficient;

            // Cast once to float for Vector3
            return new Vector3(0f, (float)lift, 0f);
        }
    }
}
