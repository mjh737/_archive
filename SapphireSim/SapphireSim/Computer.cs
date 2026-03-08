namespace ComputerSimulator
{
    using System;
    using Timer = System.Windows.Forms.Timer;

    public partial class Computer : Form
    {
        private int TICK_INTERVAL = 1000; // 1 second
        public int TickCounter { get; private set; } = 0;
        private Timer _systemClock;
        private List<IDevice> _devices;
        private Dictionary<string, IComponent> _components;

        public Computer()
        {
            InitializeComponent();

            InitializeComponents();
            InitializeDevices();

            InitializeClock();
        }

        private void InitializeDevices()
        {
            _devices = new List<IDevice>
            {

            };
        }

        private void InitializeComponents()
        {
            _components = new Dictionary<string, IComponent>()
            {
                ["cpu0"] = new Cpu(),
            };
        }

        private void InitializeClock()
        {
            _systemClock = new Timer();
            _systemClock.Interval = TICK_INTERVAL;
            _systemClock.Start();
            _systemClock.Tick += SystemClock_Tick;
        }

        private void SystemClock_Tick(object? sender, EventArgs e)
        {
            UpdateDisplay();

            foreach (var component in _components.Values)
            {
                component.Tick();
            }
        }

        private void UpdateDisplay()
        {
            TickCounter++;
            lblTickCounter.Text = $"Tick Counter: {TickCounter}";

            if (_components.TryGetValue("cpu0", out var comp) && comp is Cpu cpu)
            {
                lblIP.Text = $"IP: {cpu.IP}";
            }
            else
            {
                lblIP.Text = "IP: (CPU not found)";
            }
        }
    }
}
