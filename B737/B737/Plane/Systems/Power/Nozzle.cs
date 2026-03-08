namespace B737.Plane.Systems.Power
{
    using B737.Physics;

    public class Nozzle : IDegradable
    {
        public double Efficiency { get; set; } = 0.95;


        public void ExpandToAmbient(AirFlowPacket stream)
        {
            // Isentropic expansion to ambient pressure
            double pr = stream.Pt / Atmosphere.AmbientPressurePa; // total/ambient
            pr = Math.Max(pr, 1.001); // avoid divide-by-zero or <=1
            double exponent = (PhysicalConstants.Gamma - 1.0) / PhysicalConstants.Gamma;

            // Temperature drop factor
            double term = 1.0 - Math.Pow(1.0 / pr, exponent);

            // Exit velocity from enthalpy drop
            stream.V = Math.Sqrt(2.0 * PhysicalConstants.Cp * stream.Tt * Efficiency * term);
        }
    }
}
