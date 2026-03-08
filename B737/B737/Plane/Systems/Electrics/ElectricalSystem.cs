namespace B737.Plane.Systems.Electrics
{
    using System;

    public class ElectricalSystem : IPlaneSystem
    {
        // ===== Telemetry reference =====
        private PlaneTelemetry telemetry;

        public AcBus LeftAcBus { get; } = new AcBus();
        public AcBus RightAcBus { get; } = new AcBus();
        public DcBus DcBus { get; } = new DcBus();
        public Tru LeftTru { get; } = new Tru();
        public Tru RightTru { get; } = new Tru();
        public Inverter StandbyInverter { get; } = new Inverter();

        public bool ExternalPowerConnected { get; set; } = false;
        public bool CrossTieEnabled { get; set; } = false;

        public void Step(double dt)
        {
            // 1. Update AC buses (generators or external power)
            LeftAcBus.Step(dt);
            RightAcBus.Step(dt);

            // 2. Feed DC bus via TRUs
            LeftTru.Step(LeftAcBus, DcBus, dt);
            RightTru.Step(RightAcBus, DcBus, dt);

            // 3. Update batteries
            DcBus.Step(dt, busLoadAmps: CalculateDcLoad(), mainChargeAmps: 0.0, apuChargeAmps: 0.0);

            // 4. Standby inverter: DC → AC standby bus
            StandbyInverter.Step(DcBus, LeftAcBus, dt);

            // 5. Apply cross‑tie logic
            if (CrossTieEnabled)
            {
                // Example: tie left/right AC buses together
                double avgVoltage = (LeftAcBus.Voltage + RightAcBus.Voltage) / 2.0;
                LeftAcBus.Voltage = avgVoltage;
                RightAcBus.Voltage = avgVoltage;
            }

            // 6. Update telemetry battery voltage live
            if (telemetry != null)
            {
                telemetry.BatteryVoltage = DcBus.BusVoltage;
            }
        }

        private double CalculateDcLoad()
        {
            // Placeholder: sum avionics, lights, pumps, etc.
            return 40.0; // amps
        }

        public void InitializeTelemetry(PlaneTelemetry t)
        {
            telemetry = t;
            telemetry.BatteryVoltage = DcBus.BusVoltage;
        }
    }
}
