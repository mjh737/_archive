namespace B737.Plane.Systems.Fuel
{
    using B737.Constants;
    using B737.Physics;
    using B737.Plane;
    using B737.Plane.Systems;

    public class FuelSystem : IPlaneSystem
    {
        public Tank LeftTank { get; }
        public Tank RightTank { get; }
        public Tank CenterTank { get; }

        public double TotalFuelKg => LeftTank.CurrentLevelKg + RightTank.CurrentLevelKg + CenterTank.CurrentLevelKg;

        // ===== Telemetry reference =====
        private PlaneTelemetry telemetry;

        public FuelSystem()
        {
            // Example initial fill: wing tanks partially filled, center empty
            LeftTank = new Tank(Masses.WingTankCapacityKg, 2500);
            RightTank = new Tank(Masses.WingTankCapacityKg, 2500);
            CenterTank = new Tank(Masses.CenterTankCapacityKg, 0);
        }

        public void InitializeTelemetry(PlaneTelemetry t)
        {
            telemetry = t;
            telemetry.LeftTankKg = LeftTank.CurrentLevelKg;
            telemetry.RightTankKg = RightTank.CurrentLevelKg;
            telemetry.CenterTankKg = CenterTank.CurrentLevelKg;
        }

        /// <summary>
        /// Step the fuel system: subtract fuel burn from tanks and update telemetry.
        /// </summary>
        public void Step(double dt)
        {
            if (telemetry == null) return;

            // Burn sequence: center tank first, then wings
            double burnLeft = telemetry.FuelFlowLeft * dt;
            double burnRight = telemetry.FuelFlowRight * dt;

            // Apply burn to tanks
            BurnFromTank(CenterTank, ref burnLeft, ref burnRight);
            BurnFromTank(LeftTank, ref burnLeft, ref burnRight);
            BurnFromTank(RightTank, ref burnLeft, ref burnRight);

            // Update telemetry with current tank levels
            telemetry.LeftTankKg = LeftTank.CurrentLevelKg;
            telemetry.RightTankKg = RightTank.CurrentLevelKg;
            telemetry.CenterTankKg = CenterTank.CurrentLevelKg;
        }

        private void BurnFromTank(Tank tank, ref double burnLeft, ref double burnRight)
        {
            if (burnLeft > 0 && tank.CurrentLevelKg > 0)
            {
                double consumed = tank.ConsumeFuel(burnLeft);
                burnLeft -= consumed;
            }

            if (burnRight > 0 && tank.CurrentLevelKg > 0)
            {
                double consumed = tank.ConsumeFuel(burnRight);
                burnRight -= consumed;
            }
        }

        /// <summary>
        /// Refuel distributing amountKg: fill left/right equally until both full, then center.
        /// </summary>
        public void Refuel(double amountKg)
        {
            if (amountKg <= 0) return;
            double remaining = amountKg;

            if (LeftTank.CurrentLevelKg < LeftTank.CapacityKg && remaining > 0)
            {
                double added = LeftTank.Refuel(remaining);
                remaining -= added;
            }
            if (RightTank.CurrentLevelKg < RightTank.CapacityKg && remaining > 0)
            {
                double added = RightTank.Refuel(remaining);
                remaining -= added;
            }
            if (remaining > 0 && CenterTank.CurrentLevelKg < CenterTank.CapacityKg)
            {
                CenterTank.Refuel(remaining);
            }

            // Update telemetry after refuel
            if (telemetry != null)
            {
                telemetry.LeftTankKg = LeftTank.CurrentLevelKg;
                telemetry.RightTankKg = RightTank.CurrentLevelKg;
                telemetry.CenterTankKg = CenterTank.CurrentLevelKg;
            }
        }
    }
}