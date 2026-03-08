
using System;
namespace FlightSimulator
{
    public class Tank
    {
        const double FILL_RATE = 5.133;  // kg per second

        public double Capacity { get; set; }
        public string Name { get; set; }
        public bool IsValveOpen { get; set; }
        public bool ContainsFuel { get { return FuelQuantity > 0; } }
        public double FuelQuantity { get; set; } //kgs

        public void Open()
        {
            IsValveOpen = true;
        }

        public void Close()
        {
            IsValveOpen = false;
        }

        public bool IsAvailable()
        {
            return (IsValveOpen && ContainsFuel);
        }

        public void Fill(TimeSpan delta)
        {
            FuelQuantity += (FILL_RATE * delta.TotalMilliseconds / 1000);

            if (FuelQuantity > Capacity) FuelQuantity = Capacity;
        }

        /// <summary>
        /// Attempts to add fuel to this tank
        /// </summary>
        /// <param name="quantity">Quantity of fuel to add</param>
        /// <returns>Leftover fuel</returns>
        public double Fill(double quantity)
        {
            double remainder = 0;

            FuelQuantity += quantity;

            if (FuelQuantity > Capacity)
            {
                remainder = FuelQuantity - Capacity;
                FuelQuantity = Capacity;
            }

            return remainder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        /// <param name="burnRate"></param>
        /// <returns>true if able to supply fuel</returns>
        public bool BurnFuel(TimeSpan delta, double burnRate)
        {
            if (!IsValveOpen) return false;

            FuelQuantity -= (burnRate * delta.Milliseconds / 1000);

            if (FuelQuantity < 0)
            {
                FuelQuantity = 0;
                IsValveOpen = false;
                return false;
            }

            else return true;
        }
    }
}