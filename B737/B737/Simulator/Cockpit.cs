namespace B737
{
    using B737.Physics;
    using B737.Plane;

    public partial class Cockpit : Form
    {
        private readonly Airframe plane = new Airframe();
        private bool _isSyncingSliders; // guard to avoid feedback loops

        public Cockpit()
        {
            InitializeComponent();
            InitializeLabels();
            SubscribeEvents();
            UpdateEnvironmentTabControls(); // set initial values
            UpdateElectricsTabControls();   // set initial values
        }

        private void SubscribeEvents()
        {
            // Plane events
            plane.FuelBurnChanged += Plane_FuelBurnChanged;
            plane.ThrustChanged += Plane_ThrustChanged;

            // UI controls
            LeftThrottleSlider.ValueChanged += LeftThrottleSlider_ValueChanged;
            RightThrottleSlider.ValueChanged += RightThrottleSlider_ValueChanged;
            BothThrottleSlider.ValueChanged += BothThrottleSlider_ValueChanged;
            LeftFuelCutoffCheckBox.CheckedChanged += LeftFuelCutoffCheckBox_CheckedChanged;
            RightFuelCutoffCheckBox.CheckedChanged += RightFuelCutoffCheckBox_CheckedChanged;

            // Electrics
            MainBatteryOnlineCheckBox.CheckedChanged += MainBatteryOnlineCheckBox_CheckedChanged;

            // Flight dynamics net force + acceleration
            FlightDynamics.NetForceAndAccelerationComputed += FlightDynamics_NetForceAndAccelerationComputed;

            // Tab selection
            Tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        }

        private void FlightDynamics_NetForceAndAccelerationComputed(object? sender, (double forceMagnitudeN, double accelerationMs2) data)
        {
            // Derive mass from F = m * a (guard against divide-by-zero)
            double massKg = data.accelerationMs2 > 1e-6 ? data.forceMagnitudeN / data.accelerationMs2 : 0.0;

            SafeUiUpdate(() =>
            {
                NetForceLabel.Text = $"Net Force Magnitude: {data.forceMagnitudeN:F0} N";
                AccelerationLabel.Text = $"Acceleration: {data.accelerationMs2:F2} m/s²";
                MassLabel.Text = $"Mass: {massKg:F0} kg";
            });
        }

        private void Tabs_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (Tabs.SelectedTab == EnvironmentTab)
            {
                UpdateEnvironmentTabControls();
            }
            else if (Tabs.SelectedTab == ElectricsTab)
            {
                UpdateElectricsTabControls();
            }
        }

        private void InitializeLabels()
        {
            FuelLabel.Text = $"Fuel Remaining: {plane.FuelSystem.TotalFuelKg:F1} kg";
            LeftThrottleValueLabel.Text = "Left Throttle: 0.00";
            RightThrottleValueLabel.Text = "Right Throttle: 0.00";
            BothThrottleValueLabel.Text = "Both Throttle: 0.00";
            LeftN1Label.Text = "Left N1: -- %";
            RightN1Label.Text = "Right N1: -- %";
            LeftFuelFlowLabel.Text = "Left Fuel Flow: -- kg/h";
            RightFuelFlowLabel.Text = "Right Fuel Flow: -- kg/h";
            LeftThrustLabel.Text = "Left Thrust: -- N";
            RightThrustLabel.Text = "Right Thrust: -- N";
            BatteryVoltageLabel.Text = "Battery Voltage: -- V";
            NetForceLabel.Text = "Net Force Magnitude: -- N";
            AccelerationLabel.Text = "Acceleration: -- m/s²";
            MassLabel.Text = "Mass: -- kg";
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Start simulation asynchronously; do not block UI thread
            await plane.StartAsync();

            // Kick initial labels
            UpdateN1Labels();
            UpdateFuelFlowLabels();
            UpdateBatteryVoltageLabel();
        }

        // ===== Plane event handlers =====

        private void Plane_FuelBurnChanged(object? sender, (double leftKg, double rightKg) burn)
        {
            double totalBurn = burn.leftKg + burn.rightKg;
            double remaining = plane.FuelSystem.TotalFuelKg - totalBurn;

            SafeUiUpdate(() =>
            {
                FuelLabel.Text = $"Fuel Remaining: {remaining:F1} kg";
                UpdateN1Labels();
                UpdateFuelFlowLabels();
            });
        }

        private void Plane_ThrustChanged(object? sender, (double leftThrustN, double rightThrustN) thrust)
        {
            SafeUiUpdate(() => UpdateThrustAndLabels(thrust));
        }

        // ===== UI control handlers =====

        private void LeftThrottleSlider_ValueChanged(object? sender, EventArgs e)
        {
            double value = LeftThrottleSlider.Value / 100.0;
            plane.Powerplant.ThrottleLeft = value;

            if (!_isSyncingSliders)
            {
                int avg = (LeftThrottleSlider.Value + RightThrottleSlider.Value) / 2;
                if (BothThrottleSlider.Value != avg)
                {
                    _isSyncingSliders = true;
                    BothThrottleSlider.Value = avg;
                    _isSyncingSliders = false;
                }
                BothThrottleValueLabel.Text = $"Both Throttle: {(avg / 100.0):F2}";
            }

            SafeUiUpdate(() =>
            {
                LeftThrottleValueLabel.Text = $"Left Throttle: {value:F2}";
                UpdateN1Labels();
                UpdateFuelFlowLabels();
            });
        }

        private void RightThrottleSlider_ValueChanged(object? sender, EventArgs e)
        {
            double value = RightThrottleSlider.Value / 100.0;
            plane.Powerplant.ThrottleRight = value;

            if (!_isSyncingSliders)
            {
                int avg = (LeftThrottleSlider.Value + RightThrottleSlider.Value) / 2;
                if (BothThrottleSlider.Value != avg)
                {
                    _isSyncingSliders = true;
                    BothThrottleSlider.Value = avg;
                    _isSyncingSliders = false;
                }
                BothThrottleValueLabel.Text = $"Both Throttle: {(avg / 100.0):F2}";
            }

            SafeUiUpdate(() =>
            {
                RightThrottleValueLabel.Text = $"Right Throttle: {value:F2}";
                UpdateN1Labels();
                UpdateFuelFlowLabels();
            });
        }

        private void BothThrottleSlider_ValueChanged(object? sender, EventArgs e)
        {
            if (_isSyncingSliders)
                return;

            int val = BothThrottleSlider.Value;
            _isSyncingSliders = true;
            try
            {
                if (LeftThrottleSlider.Value != val) LeftThrottleSlider.Value = val;
                if (RightThrottleSlider.Value != val) RightThrottleSlider.Value = val;
            }
            finally
            {
                _isSyncingSliders = false;
            }

            double value = val / 100.0;
            BothThrottleValueLabel.Text = $"Both Throttle: {value:F2}";
        }

        private void LeftFuelCutoffCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            plane.Powerplant.FuelCutoffLeft = LeftFuelCutoffCheckBox.Checked;
            SafeUiUpdate(() =>
            {
                UpdateFuelFlowLabels();
                UpdateN1Labels();
            });
        }

        private void RightFuelCutoffCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            plane.Powerplant.FuelCutoffRight = RightFuelCutoffCheckBox.Checked;
            SafeUiUpdate(() =>
            {
                UpdateFuelFlowLabels();
                UpdateN1Labels();
            });
        }

        private void MainBatteryOnlineCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            plane.ElectricalSystem.DcBus.MainBattery.IsOnline = MainBatteryOnlineCheckBox.Checked;
            UpdateBatteryVoltageLabel();
        }

        // ===== UI update helpers =====

        private void UpdateThrustAndLabels((double leftThrustN, double rightThrustN) thrust)
        {
            LeftThrustLabel.Text = $"Left Thrust: {thrust.leftThrustN:F0} N";
            RightThrustLabel.Text = $"Right Thrust: {thrust.rightThrustN:F0} N";
            UpdateN1Labels();
            UpdateFuelFlowLabels();
            UpdateBatteryVoltageLabel();
        }

        private void UpdateN1Labels()
        {
            LeftN1Label.Text = $"Left N1: {plane.Telemetry.CurrentN1Left:F1}%";
            RightN1Label.Text = $"Right N1: {plane.Telemetry.CurrentN1Right:F1}%";
        }

        private void UpdateFuelFlowLabels()
        {
            double leftKgPerHour = plane.Telemetry.FuelFlowLeft * 3600.0;
            double rightKgPerHour = plane.Telemetry.FuelFlowRight * 3600.0;
            LeftFuelFlowLabel.Text = $"Left Fuel Flow: {leftKgPerHour:F1} kg/h";
            RightFuelFlowLabel.Text = $"Right Fuel Flow: {rightKgPerHour:F1} kg/h";
        }

        private void UpdateEnvironmentTabControls()
        {
            txtAmbientPressure.Text = Atmosphere.AmbientPressurePa.ToString("F0");
            txtAmbientTemperature.Text = Atmosphere.AmbientTemperatureK.ToString("F2");
        }

        private void UpdateElectricsTabControls()
        {
            MainBatteryOnlineCheckBox.Checked = plane.ElectricalSystem.DcBus.MainBattery.IsOnline;
            UpdateBatteryVoltageLabel();
        }

        private void UpdateBatteryVoltageLabel()
        {
            BatteryVoltageLabel.Text = $"Battery Voltage: {plane.Telemetry.BatteryVoltage:F1} V";
        }

        private void SafeUiUpdate(Action update)
        {
            if (InvokeRequired)
                BeginInvoke(update);
            else
                update();
        }
    }
}