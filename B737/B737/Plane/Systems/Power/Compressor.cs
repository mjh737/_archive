namespace B737.Plane.Systems.Power
{
    using B737.Physics;

    public class Compressor : IDegradable
    {
        public double PressureRatio { get; set; } = 20.0;
        public double Efficiency { get; set; } = 0.88;
        public void Process(AirFlowPacket core)
        {
            core.Tt = PhysicalConstants.IsentropicTempRise(core.Tt, PressureRatio, Efficiency);
            core.Pt *= PressureRatio;
        }
    }
}
