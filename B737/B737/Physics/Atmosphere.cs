namespace B737.Physics
{
    public class Atmosphere
    {
        public static double AmbientPressurePa { get; set; } = 101325.0;

        public static double AmbientTemperatureK { get; set; } = 273.15 + 15;

        public static int OutsideAirTemperatureC() => (int)(AmbientTemperatureK - 273.15);

        public static int Qnh() => (int)(AmbientPressurePa / 100.0);

        public static double SpeedOfSound() => Math.Sqrt(PhysicalConstants.Gamma * PhysicalConstants.R * Atmosphere.AmbientTemperatureK);
    }
}
