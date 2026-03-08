namespace B737.Physics
{
    // Flow packet representing either core or bypass stream
    public class AirFlowPacket
    {
        public double MassFlow; // kg/s
        public double Tt;       // Total temperature K
        public double Pt;       // Total pressure Pa
        public double V;        // Exit velocity m/s (static)
        public AirFlowPacket(double m, double Tt, double Pt)
        {
            MassFlow = m; this.Tt = Tt; this.Pt = Pt; V = 0.0;
        }
    }
}
