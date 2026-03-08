namespace B737.Plane.Systems.Power
{
    using B737.Constants;
    using B737.Physics;
    using B737.Simulator;

    public class Combustor : IDegradable
    {
        // Combustor parameters
        public double Efficiency { get; set; } = 0.99;
        public double PressureLossFactor { get; set; } = 0.95; // Pt_out = factor * Pt_in
        

        // Output telemetry
        public double LastFuelBurn { get; private set; } = 0.0; // kg/s
        public double LastDeltaT { get; private set; } = 0.0;   // K
        public double LastDeltaH { get; private set; } = 0.0;   // J/s

        /// <summary>
        /// Burns fuel to raise the core flow temperature toward the demanded turbine inlet temp.
        /// Updates core total temperature and pressure, and computes fuel flow.
        /// </summary>
        /// <param name="core">The core airflow packet.</param>
        /// <param name="demandTt4">Demanded turbine inlet temperature (K).</param>
        public void Burn(AirFlowPacket core, double demandTt4)
        {
            // Clamp demand to physical maximum
            double target = Math.Min(demandTt4, CFM56_7B27.MaxTurbineInletTempK);


            // Temperature rise required
            double deltaT = target - core.Tt;
            if (deltaT <= 0.0)
            {
                // Already hotter than demand: no burn
                LastFuelBurn = 0.0;
                LastDeltaT = 0.0;
                LastDeltaH = 0.0;
                return;
            }

            // Raise the total temperature
            core.Tt += deltaT;

            // Apply pressure loss across combustor
            core.Pt *= PressureLossFactor;

            // Energy added to the core flow
            double deltaH = PhysicalConstants.Cp * core.MassFlow * deltaT;

            // Fuel mass flow required (kg/s), efficiency applied once
            double mFuel = deltaH / (JetAFuel.LHV * Efficiency);

            // Telemetry
            LastFuelBurn = mFuel;
            LastDeltaT = deltaT;
            LastDeltaH = deltaH;
        }
    }
}