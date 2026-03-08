using System;
using System.Collections.Generic;
using System.Text;

namespace Graphite
{
    public class xFaultSettings
    {
        int minDelta; public int MinDelta { get { return minDelta; } set { minDelta = value; } }
        int maxDelta; public int MaxDelta { get { return maxDelta; } set { maxDelta = value; } }
        int iterations; public int Iterations { get { return iterations; } set { iterations = value; } }
        int iterationsPerFilter; public int IterationsPerFilter { get { return iterationsPerFilter; } set { iterationsPerFilter = value; } }
        float filterValue; public float FilterValue { get { return filterValue; } }

        public xFaultSettings()
        {
            this.minDelta = 0;
            this.maxDelta = 0;

            this.iterations = 0;
            this.iterationsPerFilter = 0;

            this.filterValue = 0.0f;
        }

        public xFaultSettings(int minDelta, int maxDelta, int iterations, int iterationsPerFilter, float filterValue)
        {
            this.minDelta = minDelta;
            this.maxDelta = maxDelta;

            this.iterations = iterations;
            this.iterationsPerFilter = iterationsPerFilter;

            this.filterValue = filterValue;
        }
    }
}
