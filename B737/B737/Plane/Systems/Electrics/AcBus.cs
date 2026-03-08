namespace B737.Plane.Systems.Electrics
{
    using System;

    public class AcBus
    {
        public double Voltage { get; set; } = 115.0;
        public double Frequency { get; set; } = 400.0;
        public double LoadKVA { get; set; }

        public void Step(double dt)
        {
            throw new NotImplementedException();
        }
    }
}
