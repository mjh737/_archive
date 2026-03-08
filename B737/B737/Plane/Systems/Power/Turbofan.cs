namespace B737.Plane.Systems.Power
{
    using System;
    using B737.Physics;

    public class Turbofan
    {
        // ===== Scheduled ranges (set once, varied by N1Norm each cycle) =====
        public double IdleCoreMassFlow { get; set; } = 8.0;       // kg/s at N1 idle
        public double MaxCoreMassFlow { get; set; } = 32.0;       // kg/s at full N1
        public double IdleBypassRatio { get; set; } = 2.0;        // lower bypass at idle
        public double MaxBypassRatio { get; set; } = 5.0;         // high bypass at full power

        public double FanPR_Base { get; set; } = 1.40;
        public double FanPR_Max { get; set; } = 1.85;
        public double HPC_PR_Base { get; set; } = 13.0;
        public double HPC_PR_Max { get; set; } = 24.0;

        public double Tt4 { get; set; } = 1550.0;
        public double N1Norm { get; set; } = 0.0;                 // set externally (0..1)

        // ===== Dynamic (computed) =====
        public double CoreMassFlow { get; set; }
        public double BypassRatio { get; set; }
        public double FanPR { get; set; }
        public double HPC_PR { get; set; }

        // ===== Calibration (optional) =====
        public bool ThrustCalibrationEnabled { get; set; } = false;
        public double IdleThrustTarget { get; set; } = 4000.0;    // N (single engine)
        public double MaxThrustTarget { get; set; } = 117000.0;   // N

        public double LastFuelFlow { get; private set; }
        public double LastRawThrust { get; private set; }
        public double LastCalibratedThrust { get; private set; }

        private readonly Fan fan;
        private readonly Compressor hpc;
        private readonly Combustor combustor;
        private readonly Turbine hpt_lpt;
        private readonly Nozzle coreNozzle;
        private readonly Nozzle bypassNozzle;

        public Turbofan()
        {
            fan = new Fan { Efficiency = 0.9 };
            hpc = new Compressor { Efficiency = 0.88 };
            combustor = new Combustor { Efficiency = 0.99, PressureLossFactor = 0.96 };
            hpt_lpt = new Turbine { Efficiency = 0.9 };
            coreNozzle = new Nozzle { Efficiency = 0.95 };
            bypassNozzle = new Nozzle { Efficiency = 0.95 };
        }

        // Scheduling called before RunCycle by Powerplant
        public void ApplyScheduling()
        {
            // Mild non-linear weighting for mass flow (idle mass even lower)
            double w = N1Norm;
            double flowShape = w * w * (3.0 - 2.0 * w); // smoothstep
            CoreMassFlow = IdleCoreMassFlow + flowShape * (MaxCoreMassFlow - IdleCoreMassFlow);
            BypassRatio = IdleBypassRatio + w * (MaxBypassRatio - IdleBypassRatio);

            FanPR = FanPR_Base + w * (FanPR_Max - FanPR_Base);
            HPC_PR = HPC_PR_Base + w * (HPC_PR_Max - HPC_PR_Base);
        }

        public (double thrustN, double coreV, double bypassV, double fuelFlow) RunCycle(double inletMach)
        {
            double T_amb = Atmosphere.AmbientTemperatureK;
            double P_amb = Atmosphere.AmbientPressurePa;

            double Tt_inlet = PhysicalConstants.StaticToTotalTemp(T_amb, inletMach);
            double Pt_inlet = PhysicalConstants.TotalPressureFromMach(P_amb, inletMach);

            fan.PressureRatio = FanPR;
            hpc.PressureRatio = HPC_PR;

            double m_core = CoreMassFlow;
            double m_byp = BypassRatio * m_core;

            var core = new AirFlowPacket(m_core, Tt_inlet, Pt_inlet);
            var bypass = new AirFlowPacket(m_byp, Tt_inlet, Pt_inlet);

            double fanTt_in_core = core.Tt;
            double fanTt_in_byp = bypass.Tt;

            fan.Process(bypass, core);

            double fanDeltaT = bypass.Tt - fanTt_in_byp;
            double fanPower = PhysicalConstants.Cp * (m_core + m_byp) * fanDeltaT;

            double hpcTt_in = core.Tt;
            hpc.Process(core);
            double compDeltaT = core.Tt - hpcTt_in;
            double compPower = PhysicalConstants.Cp * m_core * compDeltaT;

            combustor.Burn(core, Tt4);
            double fuelFlow = combustor.LastFuelBurn;
            LastFuelFlow = fuelFlow;

            double requiredShaftPower = fanPower + compPower;
            hpt_lpt.Expand(core, requiredShaftPower);

            coreNozzle.ExpandToAmbient(core);
            bypassNozzle.ExpandToAmbient(bypass);

            double a_inf = Atmosphere.SpeedOfSound();
            double V_inf = inletMach * a_inf;

            double rawThrust = m_core * (core.V - V_inf) + m_byp * (bypass.V - V_inf);
            LastRawThrust = rawThrust;

            double finalThrust = rawThrust;
            if (ThrustCalibrationEnabled)
            {
                double target = IdleThrustTarget + N1Norm * (MaxThrustTarget - IdleThrustTarget);
                double scale = target / Math.Max(rawThrust, 1.0);
                finalThrust = rawThrust * scale;
            }

            LastCalibratedThrust = finalThrust;
            return (finalThrust, core.V, bypass.V, fuelFlow);
        }
    }
}