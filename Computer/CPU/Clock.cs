using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Motherboard
{
    public delegate void TickDelegate(object sender, ElapsedEventArgs e);

    public class Clock
    {
        public event TickDelegate Tick;

        Timer timer;

        public Clock()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Tick(this, null);
        }

    }
}
