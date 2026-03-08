namespace B737.Physics
{
    // Basic thermodynamic constants (air, simplified)
    public static class PhysicalConstants
    {
        public const float Gravity = 9.81f;

        public const double Gamma = 1.4;         // cp/cv for air
        public const double R = 287.0;           // J/(kg·K)
        public const double Cp = Gamma * R / (Gamma - 1.0); // J/(kg·K)
        
        public static double TotalToStaticTemp(double Tt, double M) => Tt / (1.0 + 0.5 * (Gamma - 1.0) * M * M);
        public static double StaticToTotalTemp(double Ts, double M) => Ts * (1.0 + 0.5 * (Gamma - 1.0) * M * M);
        public static double TotalPressureFromMach(double Ps, double M)
        {
            double fac = 1.0 + 0.5 * (Gamma - 1.0) * M * M;
            return Ps * Math.Pow(fac, Gamma / (Gamma - 1.0));
        }
        public static double StaticPressureFromTotal(double Pt, double M)
        {
            double fac = 1.0 + 0.5 * (Gamma - 1.0) * M * M;
            return Pt / Math.Pow(fac, Gamma / (Gamma - 1.0));
        }
        public static double IsentropicTempRise(double Tt_in, double PR, double eta_c)
        {
            // Compressor: Tt_out/Tt_in = 1 + (PR^((Gamma-1)/Gamma)-1)/eta_c
            double ideal = Math.Pow(PR, (Gamma - 1.0) / Gamma);
            return Tt_in * (1.0 + (ideal - 1.0) / eta_c);
        }
        public static double IsentropicTempDrop(double Tt_in, double PR, double eta_t)
        {
            // Turbine: Tt_out/Tt_in = 1 - eta_t * (1 - PR^((Gamma-1)/Gamma))
            double ideal = Math.Pow(PR, (Gamma - 1.0) / Gamma);
            return Tt_in * (1.0 - eta_t * (1.0 - ideal));
        }
    }
}
