
using System;
namespace FlightSimulator
{
    public class Engine
    {
        const double BURN_RATE = 0.0000124844; // 1.24844 * 10^-5 kgs per second per Newton
        const int THRUST_UNIT = 400;

        double efficiency; // %

        public double Thrust { get; set; } // Newtons

        double maxThrust;
        public double MaxThrust { get { return maxThrust; } set { maxThrust = value * efficiency; } } // Newtons
        private double ThrustRate { get { return MaxThrust / THRUST_UNIT; } }
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public string FuelSource { get; set; }

        public Engine()
        {
            FuelSource = "Offline";

            // Set Engine Efficiency
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            double r = rand.NextDouble() * 2;
            efficiency = ((float)(98 + r)) / 100;
        }

        public void IncreaseThrust()
        {
            Thrust += ThrustRate;

            if (Thrust > MaxThrust)
                Thrust = MaxThrust;
        }

        public void Start()
        {
            Start(false);
        }

        public void Start(bool isFullPower)
        {
            if (FuelSource == "Offline") IsRunning = false;
            else IsRunning = true;

            if (isFullPower) Thrust = MaxThrust;
        }

        public void ShutDown()
        {
            IsRunning = false;
            FuelSource = "Offline";
            Thrust = 0;
        }

        public void DecreaseThrust()
        {
            Thrust -= ThrustRate;

            if (Thrust < 0) 
                Thrust = 0;
        }

        public double GetFuelConsumptionRate()
        {
            if (FuelSource == "Offline") return 0;

            return BURN_RATE * Thrust / efficiency;
        }

        internal void RequestThrust(int requestedThrustPercentage)
        {
            double requestedThrust = requestedThrustPercentage * MaxThrust / 100;

            if (requestedThrust < Thrust) DecreaseThrust();
            if (requestedThrust > Thrust) IncreaseThrust();
        }


    }
}
