namespace B737.Physics
{
    using B737.Constants;
    using B737.Plane;
    using System.Numerics; // for Vector3

    public class AircraftState
    {
        // --- Environment ---
        //public double AirDensity { get; set; } = 1.225; // kg/m^3 at sea level
        public double AltitudeM { get; set; } = 0.0;
        public double FuelMassKg { get; set; } = 0.0;

        // --- Kinematics ---
        public Vector3 Velocity { get; set; } = Vector3.Zero; // m/s in body axes
        public Vector3 Position { get; set; } = Vector3.Zero; // world coordinates
        public Vector3 Orientation { get; set; } = Vector3.Zero; // pitch, roll, yaw (rad)

        // --- Derived values ---
        public double Speed => Velocity.Length();
        public double VelocitySquared => Velocity.LengthSquared();

        // Angle of attack (simplified: pitch vs flight path)
        public double AngleOfAttackRad { get; set; } = 0.0;

        public double TotalMassKg => Masses.EmptyMassKg + FuelMassKg + WeightsAndBalances.PaxMassKg + WeightsAndBalances.CargoMassKg;

        // Mach number (for compressibility effects)
        //public double Mach => Speed / Atmosphere.SpeedOfSound();

        //private double SpeedOfSound()
        //{
        //    // Simplified ISA model: ~340 m/s at sea level
        //    return 340.0 - 0.003 * AltitudeM;
        //}
    }
}
