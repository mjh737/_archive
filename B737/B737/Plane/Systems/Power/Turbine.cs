namespace B737.Plane.Systems.Power
{
    using B737.Physics;
    using System;

    public class Turbine : IDegradable
    {
        public double Efficiency { get; set; } = 0.9;
        public double MechanicalFractionToFan { get; set; } = 0.6; // fraction of core power used to drive fan/compressor

        // Simplified: we pick a pressure ratio to extract needed power.
        public void Expand(AirFlowPacket core, double requiredShaftPower)
        {
            // Core power available from turbine: Cp * m * (Tt_in - Tt_out)
            // Choose PR so that Cp*m*ΔT ≈ requiredShaftPower / Efficiency
            double m = core.MassFlow;
            double Tt_in = core.Tt;
            double deltaT_needed = requiredShaftPower / (PhysicalConstants.Cp * m) / Efficiency;
            double Tt_out = Math.Max(900.0, Tt_in - deltaT_needed); // clamp to avoid freezing

            // Infer PR from isentropic relation inverted
            double tempRatioIdeal = Tt_out / Tt_in;
            double PR = Math.Pow(1.0 - (1.0 - tempRatioIdeal) / Efficiency, PhysicalConstants.Gamma / (PhysicalConstants.Gamma - 1.0));
            core.Tt = Tt_out;
            core.Pt *= Math.Max(0.2, PR); // avoid zero/negative
        }
    }
}
