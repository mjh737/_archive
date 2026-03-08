namespace B737.Physics
{
    using System.Numerics;
    using System;

    public class FlightDynamics
    {
        private readonly List<IForce> forces = new();

        // Raised after NetForce is computed; provides |total| (N) and acceleration (m/s^2)
        public static event EventHandler<(double forceMagnitudeN, double accelerationMs2)>? NetForceAndAccelerationComputed;

        public void AddForce(IForce force) => forces.Add(force);

        public Vector3 NetForce(AircraftState state)
        {
            Vector3 total = Vector3.Zero;
            foreach (var f in forces)
                total += f.CalculateForce(state);

            // Compute acceleration from total mass
            double forceMag = total.Length();
            double accel = state.TotalMassKg > 0.0 ? forceMag / state.TotalMassKg : 0.0;

            // Notify listeners
            NetForceAndAccelerationComputed?.Invoke(this, (forceMag, accel));

            return total;
        }

        public Vector3 NetMoment(AircraftState state)
        {
            Vector3 total = Vector3.Zero;
            foreach (var f in forces.OfType<Thrust>())
                total += f.CalculateMoment(state);
            return total;
        }
    }

    // Similarly: Drag, Thrust
}
