
using System;
using System.Collections.Generic;
namespace FlightSimulator
{
    public class FuelSystem
    {
        public Tank[] Tanks { get; set; }
        private string tankInUse;
        public int FuelMass { get { return RemainingFuel(); } }
        AircraftType aircraft;

        public bool IsFuelAvailable
        { get
            {
                foreach (Tank tank in Tanks)
                {
                    if (tank.IsAvailable()) return true;
                }

                return false;
            }
        }

        public FuelSystem(AircraftType aircraft)
        {
            this.aircraft = aircraft;

            switch (aircraft)
            {
                case AircraftType.Boeing747400:
                {
                    Tanks = new Tank[8];

                    Tanks[0] = new Tank() { Name = "Main1", Capacity =  13200, isOpen = false };
                    Tanks[1] = new Tank() { Name = "Main2", Capacity = 38011, isOpen = false };
                    Tanks[2] = new Tank() { Name = "Main3", Capacity = 38011, isOpen = false };
                    Tanks[3] = new Tank() { Name = "Main4", Capacity = 13200, isOpen = false };
                    Tanks[4] = new Tank() { Name = "Reserve2", Capacity = 3991, isOpen = false };
                    Tanks[5] = new Tank() { Name = "Reserve3", Capacity = 3991, isOpen = false };
                    Tanks[6] = new Tank() { Name = "Center", Capacity = 54024, isOpen = false };
                    Tanks[7] = new Tank() { Name = "Stabilizer", Capacity = 10387, isOpen = false };

                    break;
                }
            }

            SetTankInUse("Main1");
        }

        private void SetTankInUse(string tankToUse)
        {
            tankInUse = tankToUse;
        }

        public double Capacity()
        {
            double capacity = 0;

            foreach(Tank tank in Tanks)
            {
                capacity += tank.Capacity;
            }

            return capacity;
        }

        public void OpenFuelTankValve(string Name)
        {
            foreach (Tank tank in Tanks)
            {
                if (tank.Name == Name) tank.OpenTankValve();
            }
        }

        public void CloseFuelTankValve(string Name)
        {
            foreach (Tank tank in Tanks)
            {
                if (tank.Name == Name) tank.CloseTankValve();
            }
        }

        public void Update(TimeSpan delta, Dictionary<string, double> usage)
        {
            double rate = 0;

            foreach (Tank tank in Tanks)
            {
                usage.TryGetValue(tank.Name, out rate);
                tank.Drain(delta, rate);
            }
        }

        public int RemainingFuelIn(string name)
        {
            foreach (Tank tank in Tanks)
            {
                if (tank.Name == name) return (int)tank.Contents;
            }

            return 0;
        }

        public int RemainingFuel()
        {
            int fuel = 0;

            foreach (Tank tank in Tanks)
            {
                fuel += (int)tank.Contents;
            }

            return fuel;
        }

        public void UploadFuel(TimeSpan delta, string tankToFill)
        {
            foreach (Tank tank in Tanks)
            {
                if (tank.Name == tankToFill)
                    tank.Fill(delta);
            }
        }

        public void UploadFuel(int quantity)
        {
            switch (aircraft)
            {
                case AircraftType.Boeing747400:
                {
                    if (quantity > 0)
                    {
                        int half = quantity / 2;

                        UploadToTank("Main2", half);
                        UploadToTank("Main3", half);

                        quantity = quantity - RemainingFuelIn("Main2") - RemainingFuelIn("Main3");
                    }

                    if (quantity > 0)
                    {
                        int half = quantity / 2;

                        UploadToTank("Main1", half);
                        UploadToTank("Main4", half);

                        quantity = quantity - RemainingFuelIn("Main1") - RemainingFuelIn("Main2") - RemainingFuelIn("Main3") - RemainingFuelIn("Main4");
                    }

                    if (quantity > 0)
                    {
                        UploadToTank("Center", quantity);

                        quantity = quantity - RemainingFuelIn("Main1") - RemainingFuelIn("Main2") - RemainingFuelIn("Main3") - RemainingFuelIn("Main4") - RemainingFuelIn("Center");
                    }

                    if (quantity > 0)
                    {
                        UploadToTank("Stabilizer", quantity);

                        quantity = quantity - RemainingFuelIn("Main1") - RemainingFuelIn("Main2") - RemainingFuelIn("Main3") - RemainingFuelIn("Main4") - RemainingFuelIn("Center") - RemainingFuelIn("Stabilizer");
                    }

                    if (quantity > 0)
                    {
                        int half = quantity / 2;

                        UploadToTank("Reserve2", half);
                        UploadToTank("Reserve3", half);
                    }
                    break;
                }
            }
        }

        private void UploadToTank(string tankToFill, int quantity)
        {
            foreach (Tank tank in Tanks)
            {
                if (tank.Name == tankToFill)
                    tank.Fill(quantity);
            }
        }
    }
}
