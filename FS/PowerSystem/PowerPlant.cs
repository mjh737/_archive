using System.Collections.Generic;
using System.Diagnostics;
namespace FlightSimulator
{
    public class PowerPlant
    {
        public Engine[] Engines { get; set; }

        public PowerPlant(AircraftType aircraft)
        {
            switch (aircraft)
            {
                case AircraftType.Boeing747400:
                {
                    Engines = new Engine[4];
                    Engines[0] = new Engine() { Number = "1", Type = "PW4056", MaxThrust = 282000, Thrust = 0 };
                    Engines[1] = new Engine() { Number = "2", Type = "PW4056", MaxThrust = 282000, Thrust = 0 };
                    Engines[2] = new Engine() { Number = "3", Type = "PW4056", MaxThrust = 282000, Thrust = 0 };
                    Engines[3] = new Engine() { Number = "4", Type = "PW4056", MaxThrust = 282000, Thrust = 0 };
                    break;
                }
            }
        }

        public void Increment()
        {
            foreach (Engine engine in Engines)
            {
                if (engine.isOn) engine.Increment();
            }
        }

        public void Increment(string engineNumber)
        {
            foreach (Engine engine in Engines)
            {
                if (engine.isOn && engine.Number == engineNumber) engine.Increment();
            }
        }

        public void Decrement()
        {
            foreach (Engine engine in Engines)
            {
                if (engine.isOn) engine.Decrement();
            }
        }

        public void Decrement(string engineNumber)
        {
            foreach (Engine engine in Engines)
            {
                if (engine.isOn && engine.Number == engineNumber) engine.Decrement();
            }
        }

        public Dictionary<string, double> CurrentFuelConsumption()
        {
            Dictionary<string, double> usage = new Dictionary<string, double>();

            foreach (Engine engine in Engines)
            {
                if (engine.TankFeed != "Offline") usage.Add(engine.TankFeed, engine.GetFuelConsumptionRate());
            }



            return usage;
        }

        public void SwitchOn(string engineNumber)
        {
            foreach (Engine engine in Engines)
            {
                if (engine.Number == engineNumber) engine.Start();
            }
        }

        public void SwitchOnAll()
        {
            foreach (Engine engine in Engines)
            {
                engine.Start();
            }
        }

        public void ShutDownAll()
        {
            foreach (Engine engine in Engines)
            {
                engine.ShutDown();
            }
        }

        public double ShutDown(string engineNumber)
        {
            double flowRate = 0;

            foreach (Engine engine in Engines)
            {
                flowRate += engine.GetFuelConsumptionRate();
            }

            return flowRate;
        }

        public void RequestThrust(int requestedThrustPercentage)
        {
            if (requestedThrustPercentage > 100 || requestedThrustPercentage < 0) return;

            foreach (Engine engine in Engines)
            {
                if (engine.isOn) engine.RequestThrust(requestedThrustPercentage);
            }
        }

        public double GetCurrentThrustN()
        {
            double thrust = 0;

            foreach (Engine engine in Engines)
            {
                double t = engine.Thrust;
                thrust += t;
            }

            return thrust;
        }

        public int GetCurrentThrustP()
        {
            return (int)(GetCurrentThrustN()/GetMaxThrust() * 100);
        }

        public int GetMaxThrust()
        {
            double maxThrust = 0;

            foreach (Engine engine in Engines)
            {
                maxThrust += engine.MaxThrust;
            }

            return (int)maxThrust;
        }

        public void SetDefaultFeedConfiguration()
        {
            Engines[0].TankFeed = "Main1";
            Engines[1].TankFeed = "Main2";
            Engines[2].TankFeed = "Main3";
            Engines[3].TankFeed = "Main4";
        }
    }
}
