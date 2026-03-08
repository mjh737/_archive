
using System;
using System.Collections.Generic;
namespace FlightSimulator
{
    public class FuelSystem
    {
        Tank[] Tanks { get; set; }
        //private string tankInUse;
        public double TotalFuel { get { return RemainingFuel(); } }

        public FuelSystem()
        {
            Tanks = new Tank[8];

            Tanks[0] = new Tank() { Name = "Main1", Capacity =  13200, IsValveOpen = false };
            Tanks[1] = new Tank() { Name = "Main2", Capacity = 38011, IsValveOpen = false };
            Tanks[2] = new Tank() { Name = "Main3", Capacity = 38011, IsValveOpen = false };
            Tanks[3] = new Tank() { Name = "Main4", Capacity = 13200, IsValveOpen = false };
            Tanks[4] = new Tank() { Name = "Reserve2", Capacity = 3991, IsValveOpen = false };
            Tanks[5] = new Tank() { Name = "Reserve3", Capacity = 3991, IsValveOpen = false };
            Tanks[6] = new Tank() { Name = "Center", Capacity = 54024, IsValveOpen = false };
            Tanks[7] = new Tank() { Name = "Stabilizer", Capacity = 10387, IsValveOpen = false };
        }

        public bool IsFuelAvailable(string tankName)
        {
            int tankNumber = GetTankNumber(tankName);

            if (tankNumber < 0 || tankNumber > 7) return false;

            return Tanks[tankNumber].IsAvailable();
        }

        public bool IsFuelAvailableToAllTanks()
        {
            if (IsFuelAvailable(Tanks[0].Name) && IsFuelAvailable(Tanks[1].Name) && IsFuelAvailable(Tanks[2].Name) && IsFuelAvailable(Tanks[3].Name)) return true;
            else return false;
        }

        private int GetTankNumber(string tankName)
        {
            int tankNumber = -1;

            switch(tankName)
            {
                case "Main1": tankNumber = 0;
                    break;
                case "Main2": tankNumber = 1;
                    break;
                case "Main3": tankNumber = 2;
                    break;
                case "Main4": tankNumber = 3;
                    break;
                case "Reserve2": tankNumber = 4;
                    break;
                case "Reserve3": tankNumber = 5;
                    break;
                case "Center": tankNumber = 6;
                    break;
                case "Stabilizer": tankNumber = 7;
                    break;
            }

            return tankNumber;
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

        public void OpenFuelTankValve(string tankName)
        {
            int tankNumber = GetTankNumber(tankName);

            Tanks[tankNumber].Open();
        }

        public void CloseFuelTankValve(string tankName)
        {
            int tankNumber = GetTankNumber(tankName);

            Tanks[tankNumber].Close();
        }

        public bool BurnFuel(TimeSpan delta, double burnRate, string tankName)
        {
            int tankNumber = GetTankNumber(tankName);

            return Tanks[tankNumber].BurnFuel(delta, burnRate);
        }

        public double RemainingFuelIn(string tankName)
        {
            int tankNumber = GetTankNumber(tankName);
            
            return Tanks[tankNumber].FuelQuantity;
        }

        public double RemainingFuel()
        {
            double fuel = 0;

            foreach (Tank tank in Tanks)
            {
                fuel += tank.FuelQuantity;
            }

            return fuel;
        }

        public void UploadFuel(TimeSpan delta, string tankName)
        {
            int tankNumber = GetTankNumber(tankName);

            Tanks[tankNumber].Fill(delta);
        }

        public void UploadFuel(double quantity)
        {
            if (quantity > 0)
            {
                double half = quantity / 2;

                UploadToTank("Main2", half);
                UploadToTank("Main3", half);

                quantity = quantity - RemainingFuelIn("Main2") - RemainingFuelIn("Main3");
            }

            if (quantity > 0)
            {
                double half = quantity / 2;

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
                double half = quantity / 2;

                UploadToTank("Reserve2", half);
                UploadToTank("Reserve3", half);
            }
        }

        private void UploadToTank(string tankName, double quantity)
        {
            int tankNumber = GetTankNumber(tankName);

            Tanks[tankNumber].Fill(quantity);
        }

        internal bool IsValveOpen(string tankName)
        {
            int tankNumber = GetTankNumber(tankName);

            return Tanks[tankNumber].IsValveOpen;
        }
    }
}
