namespace B737.Plane.Systems.Electrics
{
    using System;

    public class Battery
    {
        public bool IsOnline { get; set; } = true;

        public double CapacityAh { get; set; } = 60.0;
        

        public double StateOfCharge { get; private set; } = 1.0; // 0–1
        public double Voltage { get; private set; } = 24.0;

        private double peukertExponent = 1.1;
        private double chargeEfficiency = 0.95;
        private double dischargeEfficiency = 0.9;

        public void Step(double dtSeconds, double loadAmps, double chargeAmps)
        {
            if (!IsOnline)
            {
                // If offline, no load or charge applied.
                // Voltage reflects open‑circuit SOC only.
                Voltage = OpenCircuitVoltage(StateOfCharge);
                return;
            }

            // Effective discharge current (non-linear)
            double effectiveDischarge = Math.Pow(loadAmps, peukertExponent);

            // Net current
            double netCurrent = (chargeAmps * chargeEfficiency) - (effectiveDischarge / dischargeEfficiency);

            // Update SOC
            double deltaAh = netCurrent * (dtSeconds / 3600.0);
            StateOfCharge = Math.Clamp(StateOfCharge + deltaAh / CapacityAh, 0.0, 1.0);

            // Voltage curve under load
            Voltage = VoltageFromSOC(StateOfCharge, loadAmps);
        }

        private double VoltageFromSOC(double soc, double loadAmps)
        {
            double baseVoltage = 20.0 + 5.5 * soc; // 20–25.5 V curve
            double sag = 0.015 * loadAmps;         // 15 mV per amp sag
            return Math.Max(baseVoltage - sag, 18.0);
        }

        private double OpenCircuitVoltage(double soc)
        {
            // Voltage when battery is disconnected (no sag)
            return 20.0 + 5.5 * soc;
        }
    }
}