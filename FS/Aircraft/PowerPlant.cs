using System.Collections.Generic;
using System;
namespace FlightSimulator
{
    public class PowerPlant
    {
        public Engine[] Engines { get; set; }

        public PowerPlant()
        {
            Engines = new Engine[4];
            Engines[0] = new Engine() { Name = "Engine 1", MaxThrust = 282000, Thrust = 0, FuelSource = "Main1" };
            Engines[1] = new Engine() { Name = "Engine 2", MaxThrust = 282000, Thrust = 0, FuelSource = "Main2" };
            Engines[2] = new Engine() { Name = "Engine 3", MaxThrust = 282000, Thrust = 0, FuelSource = "Main3" };
            Engines[3] = new Engine() { Name = "Engine 4", MaxThrust = 282000, Thrust = 0, FuelSource = "Main4" };
        }

        public void IncreaseThrust()
        {
            foreach (Engine engine in Engines)
            {
                if (engine.IsRunning) engine.IncreaseThrust();
            }
        }

        public void IncreaseThrust(string engineName)
        {
            int engineNumber = GetEngineNumber(engineName);

            Engine engine = Engines[engineNumber];

            if (engine.IsRunning) engine.IncreaseThrust();
        }

        public void DecreaseThrust()
        {
            foreach (Engine engine in Engines)
            {
                if (engine.IsRunning) engine.DecreaseThrust();
            }
        }

        public void DecreaseThrust(string engineName)
        {
            int engineNumber = GetEngineNumber(engineName);

            Engine engine = Engines[engineNumber];

            if (engine.IsRunning) engine.DecreaseThrust();
        }

        public void SwitchOn(Engine engine, FuelSystem fuelSystem)
        {
            if (fuelSystem.IsFuelAvailable(engine.FuelSource))
                engine.Start();
        }

        public void SwitchOnAll(FuelSystem fuelSystem)
        {
            SwitchOnAll(fuelSystem, false);
        }

        public void SwitchOnAll(FuelSystem fuelSystem, bool isFullPower)
        {
            if (fuelSystem.IsFuelAvailableToAllTanks())
            {
                foreach (Engine engine in Engines)
                {
                    if (isFullPower) engine.Start(true);
                    else engine.Start();
                }
            }
        }

        public void ShutDownAll()
        {
            foreach (Engine engine in Engines)
            {
                engine.ShutDown();
            }
        }

        public void ShutDown(Engine engine)
        {
            engine.ShutDown();
        }

        public void RequestThrust(int requestedThrustPercentage)
        {
            if (requestedThrustPercentage > 100 || requestedThrustPercentage < 0) return;

            foreach (Engine engine in Engines)
            {
                if (engine.IsRunning) engine.RequestThrust(requestedThrustPercentage);
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
            Engines[0].FuelSource = "Main1";
            Engines[1].FuelSource = "Main2";
            Engines[2].FuelSource = "Main3";
            Engines[3].FuelSource = "Main4";
        }

        public void SetFuelSource(string engineName, string fuelSource)
        {
            int engineNumber = GetEngineNumber(engineName);

            Engines[engineNumber].FuelSource = fuelSource;
        }

        private int GetEngineNumber(string engineName)
        {
            switch(engineName)
            {
                case "Engine 1": return 0;
                case "Engine 2": return 1;
                case "Engine 3": return 2;
                case "Engine 4": return 3;
            }

            return 0;
        }

        public string GetFuelSource(string engineName)
        {
            int engineNumber = GetEngineNumber(engineName);

            return Engines[engineNumber].FuelSource;
        }

        public Engine GetEngineFromEngineName(string engineName)
        {
            foreach (Engine engine in Engines)
            {
                if (engine.Name == engineName) return engine;
            }

            return null;
        }

        public delegate bool BurnFuel(TimeSpan delta, double burnRate, string tankName);

        public void UpdateAllEngines(BurnFuel fn, TimeSpan delta)
        {
            foreach (Engine engine in Engines)
            {
                if (engine.IsRunning)
                {
                    if(!fn(delta, engine.GetFuelConsumptionRate(), engine.FuelSource)) engine.ShutDown();
                }
            }
        }
    }
}
