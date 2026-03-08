
using System;
using System.Diagnostics;
namespace FlightSimulator
{
    public class Engine
    {
        const double BURN_RATE = 0.0000124844; // 1.24844 * 10^-5 kgs per second per Newton

        double fuelEfficiency; // %
        double engineEfficiency; // %

        public double Thrust { get; set; } // Newtons

        double maxThrust;
        public double MaxThrust { get { return maxThrust; } 
            set { maxThrust = value * engineEfficiency; } } // Newtons
        public string Type { get; set; }
        public string Number { get; set; }
        public bool isOn { get; set; }
        public string TankFeed { get; set; }


        public Engine()
        {
            TankFeed = "Offline";

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            double r = rand.NextDouble() * 2;
            engineEfficiency = ((float)(97 + r)) / 100;

            r = rand.NextDouble() * 3;
            fuelEfficiency = ((float)(96 + r)) / 100;
        }

        public void Increment()
        {
            Thrust += (MaxThrust / 100);

            if (Thrust > MaxThrust)
                Thrust = MaxThrust;
        }

        public void Start()
        {
            if (TankFeed == "Offline") isOn = false;
            else isOn = true;
        }

        public void ShutDown()
        {
            isOn = false;
            TankFeed = "Offline";
            Thrust = 0;
        }

        public void Decrement()
        {
            Thrust -= MaxThrust / 100;

            if (Thrust < 0) 
                Thrust = 0;
        }

        public double GetFuelConsumptionRate()
        {
            if (TankFeed == "Offline" || !isOn)
            {
                isOn = false;
                return 0;
            }

            return BURN_RATE * Thrust / fuelEfficiency;
        }

        internal void RequestThrust(int requestedThrustPercentage)
        {
            double requestedThrust = requestedThrustPercentage * MaxThrust / 100;

            if (requestedThrust < Thrust) Decrement();
            if (requestedThrust > Thrust) Increment();
        }
    }
}
