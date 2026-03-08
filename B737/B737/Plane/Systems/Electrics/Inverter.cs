namespace B737.Plane.Systems.Electrics
{
    public class Inverter
    {
        public double Efficiency { get; set; } = 0.9;
        public void Step(DcBus dcBus, AcBus acStandby, double dt)
        {
            // Provide AC standby bus voltage from DC battery bus if needed
            acStandby.Voltage = dcBus.BusVoltage * Efficiency;
        }
    }
}
