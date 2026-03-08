namespace B737.Physics
{
    using System.Numerics;

    public class Weight : IForce
    {
        public Vector3 CalculateForce(AircraftState state)
        {
            float mass = (float)state.TotalMassKg;

            return new Vector3(0, -mass * PhysicalConstants.Gravity, 0);
        }
    }
}
