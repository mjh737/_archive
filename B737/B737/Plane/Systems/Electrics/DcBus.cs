namespace B737.Plane.Systems.Electrics
{
    using System;

    public class DcBus
    {
        public Battery MainBattery { get; } = new Battery();
        public Battery ApuBattery { get; } = new Battery();

        public bool CrossTieEnabled { get; set; } = false;

        public void Step(double dtSeconds, double busLoadAmps, double mainChargeAmps, double apuChargeAmps)
        {
            double loadMain = 0.0;
            double loadApu = 0.0;

            if (CrossTieEnabled)
            {
                // Split load between online batteries only
                int onlineCount = (MainBattery.IsOnline ? 1 : 0) + (ApuBattery.IsOnline ? 1 : 0);

                if (onlineCount > 0)
                {
                    double perBatteryLoad = busLoadAmps / onlineCount;
                    if (MainBattery.IsOnline) loadMain = perBatteryLoad;
                    if (ApuBattery.IsOnline) loadApu = perBatteryLoad;
                }
            }
            else
            {
                // Default: main battery drives bus if online, else APU battery
                if (MainBattery.IsOnline)
                    loadMain = busLoadAmps;
                else if (ApuBattery.IsOnline)
                    loadApu = busLoadAmps;
            }

            // Step each battery only if online
            if (MainBattery.IsOnline)
                MainBattery.Step(dtSeconds, loadMain, mainChargeAmps);

            if (ApuBattery.IsOnline)
                ApuBattery.Step(dtSeconds, loadApu, apuChargeAmps);
        }

        public double BusVoltage
        {
            get
            {
                if (CrossTieEnabled)
                {
                    // If both online, weakest sets bus voltage
                    if (MainBattery.IsOnline && ApuBattery.IsOnline)
                        return Math.Min(MainBattery.Voltage, ApuBattery.Voltage);
                    if (MainBattery.IsOnline) return MainBattery.Voltage;
                    if (ApuBattery.IsOnline) return ApuBattery.Voltage;
                    return 0.0; // no battery online
                }
                else
                {
                    if (MainBattery.IsOnline) return MainBattery.Voltage;
                    if (ApuBattery.IsOnline) return ApuBattery.Voltage;
                    return 0.0;
                }
            }
        }
    }
}