namespace B737.Systems.Power
{
    using B737.Plane;
    using B737.Plane.Systems;
    using B737.Plane.Systems.Power;
    using System;

    public class Powerplant : IPlaneSystem
    {
        public double ThrottleLeft { get; set; } = 0.0;
        public double ThrottleRight { get; set; } = 0.0;

        public double IdleN1 { get; set; } = 25.0;
        public double MaxN1 { get; set; } = 100.0;
        public double SpoolUpRate { get; set; } = 18.0;
        public double SpoolDownRate { get; set; } = 10.0;

        public double IdleTt4 { get; set; } = 1050.0;
        public double MaxTt4 { get; set; } = 1700.0;

        public double FuelScale { get; set; } = 1.0;
        public double IdleFuelFlowFloor { get; set; } = 0.20;

        public bool FuelCutoffLeft { get; set; } = false;
        public bool FuelCutoffRight { get; set; } = false;

        private double _currentN1Left;
        private double _currentN1Right;

        public Turbofan LeftEngine { get; }
        public Turbofan RightEngine { get; }

        public PlaneTelemetry Telemetry { get; set; }
        private readonly Random rng = new Random();

        // Variability state for each engine
        private double currentVariationLeft = 1.0;
        private double targetVariationLeft = 1.0;
        private double variationTimerLeft = 0.0;

        private double currentVariationRight = 1.0;
        private double targetVariationRight = 1.0;
        private double variationTimerRight = 0.0;

        // Smoothed thrust state
        private double smoothedThrustLeft = 0.0;
        private double smoothedThrustRight = 0.0;

        public void InitializeTelemetry(PlaneTelemetry t)
        {
            Telemetry = t;
            Telemetry.CurrentN1Left = IdleN1;
            Telemetry.CurrentN1Right = IdleN1;
        }

        public Powerplant()
        {
            _currentN1Left = IdleN1;
            _currentN1Right = IdleN1;

            LeftEngine = new Turbofan { Tt4 = IdleTt4 };
            RightEngine = new Turbofan { Tt4 = IdleTt4 };
        }

        public void Step(double dt)
        {
            if (Telemetry == null) return;

            StepOne(LeftEngine, ThrottleLeft, !FuelCutoffLeft, ref _currentN1Left, dt,
                ref currentVariationLeft, ref targetVariationLeft, ref variationTimerLeft, ref smoothedThrustLeft,
                out double thrustL, out double coreVL, out double bypassVL, out double fuelL);

            StepOne(RightEngine, ThrottleRight, !FuelCutoffRight, ref _currentN1Right, dt,
                ref currentVariationRight, ref targetVariationRight, ref variationTimerRight, ref smoothedThrustRight,
                out double thrustR, out double coreVR, out double bypassVR, out double fuelR);

            fuelL = Math.Max(fuelL * FuelScale, IdleFuelFlowFloor);
            fuelR = Math.Max(fuelR * FuelScale, IdleFuelFlowFloor);

            if (FuelCutoffLeft) { fuelL = 0.0; thrustL = 0.0; }
            if (FuelCutoffRight) { fuelR = 0.0; thrustR = 0.0; }

            Telemetry.CurrentN1Left = _currentN1Left;
            Telemetry.CurrentN1Right = _currentN1Right;
            Telemetry.ThrustLeftN = thrustL;
            Telemetry.ThrustRightN = thrustR;
            Telemetry.FuelFlowLeft = fuelL;
            Telemetry.FuelFlowRight = fuelR;
            Telemetry.CoreVLeft = coreVL;
            Telemetry.BypassVLeft = bypassVL;
            Telemetry.CoreVRight = coreVR;
            Telemetry.BypassVRight = bypassVR;
            Telemetry.FuelBurnLeftTotal += fuelL * dt;
            Telemetry.FuelBurnRightTotal += fuelR * dt;
        }

        private void StepOne(
            Turbofan engine,
            double throttle,
            bool fuelAvailable,
            ref double currentN1,
            double dt,
            ref double currentVariation,
            ref double targetVariation,
            ref double variationTimer,
            ref double smoothedThrust,
            out double thrustN,
            out double coreV,
            out double bypassV,
            out double fuelFlow
        )
        {
            double targetN1 = IdleN1 + throttle * (MaxN1 - IdleN1);
            targetN1 = Math.Clamp(targetN1, IdleN1, MaxN1);

            double n1Diff = targetN1 - currentN1;
            double rate = n1Diff >= 0 ? SpoolUpRate : SpoolDownRate;
            double n1DeltaAllowed = rate * dt;
            double n1Delta = Math.Clamp(n1Diff, -n1DeltaAllowed, n1DeltaAllowed);

            double windmillFloor = 5.0;
            double minN1 = fuelAvailable ? IdleN1 : windmillFloor;
            currentN1 = Math.Clamp(currentN1 + n1Delta, minN1, MaxN1);

            double n1Norm = (currentN1 - IdleN1) / (MaxN1 - IdleN1);
            n1Norm = Math.Clamp(n1Norm, 0.0, 1.0);

            double tt4Norm = n1Norm * n1Norm * (3.0 - 2.0 * n1Norm);
            double demandedTt4 = IdleTt4 + tt4Norm * (MaxTt4 - IdleTt4);
            engine.Tt4 = Math.Clamp(demandedTt4, IdleTt4, MaxTt4);

            var (_, coreVel, bypassVel, fuel) = engine.RunCycle(Telemetry.Mach);
            coreV = coreVel;
            bypassV = bypassVel;
            fuelFlow = fuel;

            // Compute raw thrust with continuous drift variability
            double rawThrust = CalibratedThrust(currentN1, dt, ref currentVariation, ref targetVariation, ref variationTimer);

            // Smooth thrust output
            thrustN = SmoothThrust(rawThrust, ref smoothedThrust);

            if (!fuelAvailable)
            {
                fuelFlow = 0.0;
                thrustN = 0.0;
            }
        }

        private double CalibratedThrust(double n1Percent, double dt,
                                        ref double currentVariation,
                                        ref double targetVariation,
                                        ref double variationTimer)
        {
            double[] n1 = { 25, 40, 60, 85, 100 };
            double[] thrust = { 4000, 18000, 45000, 95000, 117000 }; // Newtons

            double baseThrust = thrust[thrust.Length - 1];
            for (int i = 0; i < n1.Length - 1; i++)
            {
                if (n1Percent >= n1[i] && n1Percent <= n1[i + 1])
                {
                    double t = (n1Percent - n1[i]) / (n1[i + 1] - n1[i]);
                    baseThrust = thrust[i] + t * (thrust[i + 1] - thrust[i]);
                    break;
                }
            }

            // Update target variation every ~5 seconds
            variationTimer += dt;
            if (variationTimer > 5.0)
            {
                variationTimer = 0.0;
                double variability = (n1Percent < 40) ? 0.03 : (n1Percent < 85 ? 0.02 : 0.01);
                targetVariation = 1.0 + (rng.NextDouble() * 2 * variability - variability);
            }

            // Drift current variation smoothly toward target
            double driftRate = 0.2; // smaller = slower drift
            currentVariation += (targetVariation - currentVariation) * driftRate * dt;

            return baseThrust * currentVariation;
        }

        private double SmoothThrust(double newThrust, ref double smoothedThrust)
        {
            double alpha = 0.03; // stronger damping for smoother display
            smoothedThrust = alpha * newThrust + (1 - alpha) * smoothedThrust;
            return smoothedThrust;
        }
    }
}