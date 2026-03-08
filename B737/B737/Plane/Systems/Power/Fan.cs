namespace B737.Plane.Systems.Power
{
    using B737.Physics;

    // Components (simplified)
    public class Fan : IDegradable
    {
        public double PressureRatio { get; set; } = 1.5;
        public double Efficiency { get; set; } = 0.9;

        public void Process(AirFlowPacket bypass, AirFlowPacket core)
        {
            // Apply similar fan PR on both streams; fan raises Tt via isentropic relation adjusted by efficiency
            bypass.Tt = PhysicalConstants.IsentropicTempRise(bypass.Tt, PressureRatio, Efficiency);
            bypass.Pt *= PressureRatio;

            core.Tt = PhysicalConstants.IsentropicTempRise(core.Tt, PressureRatio, Efficiency);
            core.Pt *= PressureRatio;
        }
    }
}
