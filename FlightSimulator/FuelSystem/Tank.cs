
using System;
namespace FlightSimulator
{
    public class Tank
    {
        const double ingressRate = 5.133;  // kg per second

        public double Capacity { get; set; }
        public string Name { get; set; }
        public bool isOpen { get; set; }
        public double Contents { get; set; } //kgs

        public void OpenTankValve()
        {
            isOpen = true;
        }

        public void CloseTankValve()
        {
            isOpen = false;
        }

        public bool IsAvailable()
        {
            return (isOpen && Contents > 0);
        }

        public void Fill(TimeSpan delta)
        {
            Contents += (ingressRate * delta.TotalMilliseconds / 1000);

            if (Contents > Capacity) Contents = Capacity;
        }

        public void Fill(int quantity)
        {
            Contents += quantity;

            if (Contents > Capacity) Contents = Capacity;
        }

        public void Drain(TimeSpan delta, double egressRate)
        {
            Contents -= (egressRate * delta.Milliseconds / 1000);

            if (Contents < 0)
            {
                Contents = 0;
                isOpen = false;
            }
        }
    }
}