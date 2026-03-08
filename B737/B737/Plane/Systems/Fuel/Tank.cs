namespace B737.Plane.Systems.Fuel
{
    using System;

    public class Tank
    {
        public double CapacityKg { get; private set; }
        public double CurrentLevelKg { get; private set; }

        public Tank(double capacityKg, double initialLevelKg = 0)
        {
            CapacityKg = capacityKg;
            CurrentLevelKg = Math.Clamp(initialLevelKg, 0, CapacityKg);
        }
        public double ConsumeFuel(double amountKg)
        {
            double fuelConsumed = Math.Min(amountKg, CurrentLevelKg);
            CurrentLevelKg -= fuelConsumed;
            return fuelConsumed;
        }
        public double Refuel(double amountKg)
        {
            double space = CapacityKg - CurrentLevelKg;
            double added = Math.Min(space, amountKg);
            CurrentLevelKg += added;
            return added; // return actually added amount
        }
    }
}
