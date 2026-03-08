namespace B737.Plane
{
    using B737.Physics;
    using B737.Plane.Systems.Electrics;
    using B737.Plane.Systems.Fuel;
    using B737.Simulator;
    using B737.Systems.Power;
    using System;
    using System.Diagnostics;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;
    using static System.Windows.Forms.AxHost;

    public class Airframe
    {
        // ===== Systems =====
        public Powerplant Powerplant { get; }
        public ElectricalSystem ElectricalSystem { get; }
        public FuelSystem FuelSystem { get; }
        public PlaneTelemetry Telemetry { get; } = new PlaneTelemetry();
        public FlightDynamics dynamics;

        private readonly AircraftState state = new AircraftState();


        public event EventHandler<(double leftKg, double rightKg)>? FuelBurnChanged;
        public event EventHandler<(double leftThrustN, double rightThrustN)>? ThrustChanged;
        public event EventHandler<(double altitudeFt, double airspeedKt, double mach)>? FlightDynamicsChanged;

        public Airframe()
        {
            Powerplant = new Powerplant();
            Powerplant.InitializeTelemetry(Telemetry);

            FuelSystem = new FuelSystem();
            FuelSystem.InitializeTelemetry(Telemetry);

            ElectricalSystem = new ElectricalSystem();
            ElectricalSystem.InitializeTelemetry(Telemetry);

            dynamics = new FlightDynamics();

            //dynamics.AddForce(new Weight());
            dynamics.AddForce(new Lift(0.5)); // wing area, Cl
            dynamics.AddForce(new Drag(0.02));

            // Add both engines as thrust sources
            dynamics.AddForce(new Thrust(Powerplant, true, new Vector3(5f, 0f, -3f))); // left engine
            dynamics.AddForce(new Thrust(Powerplant, false, new Vector3(5f, 0f, +3f))); // right engine
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(10));
            var sw = Stopwatch.StartNew();

            double uiInterval = 1.0 / SimulatorSettings.UiUpdateHz;
            double lastUiUpdate = 0.0;

            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                // Stop after 1 hour simulated time
                if (SimulatorSettings.SimTime >= 3600.0)
                    break;

                double elapsedRealSeconds = sw.Elapsed.TotalSeconds;
                double targetSimTime = elapsedRealSeconds * SimulatorSettings.TimeScale;
                double step = Math.Min(0.5, targetSimTime - SimulatorSettings.SimTime);

                if (step <= 0.0)
                    continue;

                // --- Step systems ---
                Powerplant.Step(step);

                // --- Step flight dynamics ---
                Vector3 netForce = dynamics.NetForce(state);
                Vector3 netMoment = dynamics.NetMoment(state); // if you’ve added torque support
                //Integrator.Step(state, netForce, netMoment, step);

                SimulatorSettings.SimTime += step;

                // --- Raise UI events at fixed frequency ---
                if (SimulatorSettings.SimTime - lastUiUpdate >= uiInterval)
                {
                    lastUiUpdate = SimulatorSettings.SimTime;

                    // Powerplant telemetry
                    OnFuelBurnChanged(Telemetry.FuelBurnLeftTotal, Telemetry.FuelBurnRightTotal);
                    OnThrustChanged(Telemetry.ThrustLeftN, Telemetry.ThrustRightN);

                    // Flight dynamics telemetry
                    OnFlightDynamicsChanged(state.Position.Y, state.Speed, Telemetry.Mach);
                }
            }

            // Final event raise
            OnFuelBurnChanged(Telemetry.FuelBurnLeftTotal, Telemetry.FuelBurnRightTotal);
            OnThrustChanged(Telemetry.ThrustLeftN, Telemetry.ThrustRightN);
            OnFlightDynamicsChanged(state.Position.Y, state.Speed, Telemetry.Mach);
        }

        protected virtual void OnFuelBurnChanged(double leftKg, double rightKg)
            => FuelBurnChanged?.Invoke(this, (leftKg, rightKg));

        protected virtual void OnThrustChanged(double leftThrustN, double rightThrustN)
            => ThrustChanged?.Invoke(this, (leftThrustN, rightThrustN));

        protected virtual void OnFlightDynamicsChanged(double altitudeFt, double airspeedKt, double mach)
            => FlightDynamicsChanged?.Invoke(this, (altitudeFt, airspeedKt, mach));
    }
}