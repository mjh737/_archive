using System;
using System.Windows.Forms;
namespace FlightSimulator
{
    static class Simulator
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Timer timer;

            Aircraft aircraft = new Aircraft();

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(aircraft.UpdateSimulator);
            timer.Start();
            
            Application.Run(aircraft);
        }
    }
}
