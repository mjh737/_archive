namespace B737.Simulator
{
    public class SimulatorSettings
    {
        public static double SimTime { get; set; } = 0.0;          // seconds of simulated time elapsed
        public static double TimeScale { get; set; } = 1.0;         // 1.0 = real-time, <1 slower, >1 faster
        public static double UiUpdateHz { get; set; } = 10.0;       // UI updates per simulated second

        
    }
}
