using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PC2
{
    public class PSU
    {
        static PsuStatus status = PsuStatus.POWER_BAD;

        public static PsuStatus Test()
        {
            status = PsuStatus.POWER_GOOD;

            return status;
        }
    }
}

public enum PsuStatus
{
    POWER_BAD,
    POWER_GOOD
}
