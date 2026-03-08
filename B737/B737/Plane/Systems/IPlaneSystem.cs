namespace B737.Plane.Systems
{
    using B737.Plane;

    internal interface IPlaneSystem
    {
        void InitializeTelemetry(PlaneTelemetry telemetry);
        void Step(double dt);
    }
}