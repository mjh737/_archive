namespace B737.Plane
{
    using B737.Constants;

    public class WeightsAndBalances
    {
        public const int MaxPassengers = 189;

        public static int NumAdults { get; set; }
        public static int NumChildren { get; set; }

        // --- Aircraft properties ---
        

        //public float FuelMassKg = 79015f; // kg
        public static float PaxMassKg => (NumAdults * Masses.AverageAdultMassKg + NumChildren * Masses.AverageChildMassKg); // kg
        public static float CargoMassKg = 5000f; // kg


    }
}
