namespace B737.Plane
{
    public class PlaneTelemetry
    {
        // ===== Powerplant =====
        public double CurrentN1Left { get; internal set; }
        public double CurrentN1Right { get; internal set; }
        public double ThrustLeftN { get; internal set; }
        public double ThrustRightN { get; internal set; }
        public double FuelFlowLeft { get; internal set; }     // kg/s
        public double FuelFlowRight { get; internal set; }    // kg/s
        public double FuelBurnLeftTotal { get; internal set; } // kg
        public double FuelBurnRightTotal { get; internal set; } // kg
        public double CoreVLeft { get; internal set; }
        public double BypassVLeft { get; internal set; }
        public double CoreVRight { get; internal set; }
        public double BypassVRight { get; internal set; }

        // ===== Fuel System =====
        public double LeftTankKg { get; internal set; }
        public double RightTankKg { get; internal set; }
        public double CenterTankKg { get; internal set; }

        // ===== Electrical System =====
        public double BatteryVoltage { get; internal set; }

        // ===== Flight Dynamics =====
        public double LatitudeDeg { get; internal set; }
        public double LongitudeDeg { get; internal set; }
        public double HeadingDeg { get; internal set; }
        public double PitchDeg { get; internal set; }
        public double RollDeg { get; internal set; }
        public double TrueAirspeedMs { get; internal set; } = 0;
        public double AltitudeM { get; internal set; } = 0;
        public double AltitudeFt => AltitudeM * 3.28084;
        public double AirspeedKt { get; internal set; }
        public double Mach => CalculateMach();
        public double VerticalSpeedFpm { get; internal set; }


        // ===== Environmental Variables =====
        public double AmbientTempK => CalculateAmbientTempK();

        private double CalculateAmbientTempK()
        {
            const double T0 = 288.15;     // Sea level standard temp (K)
            const double lapseRate = 0.0065; // K per meter
            const double tropopauseTemp = 216.65; // K

            if (AltitudeM < 11000.0)
            {
                return T0 - lapseRate * AltitudeM;
            }
            else
            {
                return tropopauseTemp; // constant above 11 km
            }

        }

        public double CalculateMach()
        {
            // Constants for air
            const double gamma = 1.4;          // ratio of specific heats
            const double R = 287.05;           // J/(kg·K), specific gas constant for dry air

            // Local speed of sound (m/s)
            double speedOfSound = Math.Sqrt(gamma * R * AmbientTempK);

            // Mach number = TAS / a
            return TrueAirspeedMs / speedOfSound;
        }




    }
}
