namespace B737.Physics
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    public interface IForce
    {
        Vector3 CalculateForce(AircraftState state);
    }
}
