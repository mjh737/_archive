namespace B737.Plane.Systems.Electrics
{
    public class Tru : IDegradable
    {
        public double Efficiency { get; set; } = 0.95;
        public void Step(AcBus ac, DcBus dcBus, double dt)
        {
            // Convert AC load to DC amps and feed into dcBus
            double dcAmps = (ac.LoadKVA * 1000.0 / dcBus.BusVoltage) * Efficiency;
            dcBus.Step(dt, busLoadAmps: dcAmps, mainChargeAmps: 0.0, apuChargeAmps: 0.0);
        }
    }

    
}
