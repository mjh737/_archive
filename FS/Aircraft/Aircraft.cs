using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlightSimulator
{
    public partial class Aircraft : Form
    {
        const int EMPTY_MASS = 180985;
        const int SINGLE_PAX_MASS = 102;
        const float GRAVITATIONAL_CONSTANT = 9.80665F;
        const float METRES_PER_SECOND_TO_KNOTS = 0.51444444444F;
        const float METRES_PER_SECOND_TO_FEET_PER_MINUTE = 196.85F;
        const float FEET_PER_MINUTE_TO_METRES_PER_SECOND = 1 / 196.85F;
        const float METRES_TO_FEET = 3.28F;
        const int MTOW = 412770;
        const double WING_AREA = 511;
        const double KELVIN_TO_CELSIUS = -273.15;
        const double T0 = 288.15;
        const double P0 = 101325.0;
        const double M0 = 28.9644; // Mean Molecular weight of air
        const double RS = 8314.32; // gas constant
        const double AS = GRAVITATIONAL_CONSTANT * M0 / RS;
        const double CD0 = 0.022;
        const double Cdi = 0.0331;
        const int MAX_PITCH = 15;
        const int MIN_PITCH = -15;

        double Temperature { get { return T0 - 0.0065 * altitude; } }
        double Pressure { get { return P0 * Math.Exp(AS * Math.Log(T0 / Temperature) / (-0.0065)); } }
        double Density { get { return (Pressure / Temperature) * (M0 / RS); } }
        double AltitudeThrustEfficiency { get { return Pressure / P0; } }
        double Cl { get { return 0.3 + 0.1 * AoA; } }
        public double Mass { get { return EMPTY_MASS + cargoMass + paxMass + fuelSystem.TotalFuel; } }
        
        double heading = 0;
        double Heading
        {
            get { return heading; }
            set
            {
                if (value >= 360) heading = value - 360;
                else if (value <= 0) heading = value + 360;
                else heading = value;
            }
        }
        bool landingGear = true;
        int windDirection = 0;
        int windSpeed = 0;
        bool isBrakeOn = true;
        int paxMass = 0;
        int numPax = 0;
        int cargoMass = 0;
        double velocity = 0;
        double acceleration = 0;
        double remainingFuel = 0;
        double thrust = 0;
        double TotalDrag = 0;
        double friction = 0;
        double AoA = 0;
        double downwardsLift = 0;
        double lateralForce = 0;
        double vAcceleration = 0;
        double roc = 0;
        double altitude = 0; // Metres
        int ApAltROC = 1800;
        double bankAngle = 0;
        double BankAngle
        {
            get { return bankAngle; }
            set
            {
                if (value > 30) bankAngle = 30;
                if (value < -30) bankAngle = -30;
                bankAngle = value;
            }
        }

        FuelSystem fuelSystem;
        PowerPlant powerPlant;
        DateTime now = DateTime.Now;
        DateTime startRollOut;
        TimeSpan delta;

        public Aircraft()
        {
            InitializeComponent();
            SetInitialConditions();

            KeyPreview = true;
        }

        private void SetInitialConditions()
        {
            fuelSystem = new FuelSystem();
            powerPlant = new PowerPlant();

            cboSource1.DataSource = new string[] { "Offline", "Main1", "Main2", "Main3", "Main4", "Reserve2", "Reserve3", "Center", "Stabilizer" };
            cboSource2.DataSource = new string[] { "Offline", "Main1", "Main2", "Main3", "Main4", "Reserve2", "Reserve3", "Center", "Stabilizer" };
            cboSource3.DataSource = new string[] { "Offline", "Main1", "Main2", "Main3", "Main4", "Reserve2", "Reserve3", "Center", "Stabilizer" };
            cboSource4.DataSource = new string[] { "Offline", "Main1", "Main2", "Main3", "Main4", "Reserve2", "Reserve3", "Center", "Stabilizer" };

            powerPlant.SetDefaultFeedConfiguration();

            cboSource1.SelectedItem = powerPlant.Engines[0].FuelSource;
            cboSource2.SelectedItem = powerPlant.Engines[1].FuelSource;
            cboSource3.SelectedItem = powerPlant.Engines[2].FuelSource;
            cboSource4.SelectedItem = powerPlant.Engines[3].FuelSource;

            fuelSystem.UploadFuel(750000);
            
            ThrustLever.Value = 98;
            cargoMass = 0;

            numPax = 400;
            paxMass = numPax * SINGLE_PAX_MASS;
            tbEmpty.Text = EMPTY_MASS.ToString() + " kgs";
            tbPax.Text = paxMass.ToString() + " kgs";
            tbCargo.Text = cargoMass.ToString() + " kgs";

            if (Mass > MTOW) MessageBox.Show("MTOW Exceeded!");

            fuelSystem.OpenFuelTankValve("Main1");
            fuelSystem.OpenFuelTankValve("Main2");
            fuelSystem.OpenFuelTankValve("Main3");
            fuelSystem.OpenFuelTankValve("Main4");
            powerPlant.SwitchOnAll(fuelSystem, true);
            SetBrake(false);
            velocity = 75; // 155 kts ish
            
            cbApMASTER.Checked = true;
            cbApVS.Checked = true;
            tbApVS.Text = "1800";
            //cbApALT.Checked = true;
            //tbApALT.Text = "300";
        }

        public void UpdateSimulator(object sender, EventArgs e)
        {
            DateTime then = now;
            now = DateTime.Now;
            delta = now - then;

            UpdateData();
            UpdateSystems();
            UpdateDisplay();
        }

        void UpdateData()
        {
            remainingFuel = fuelSystem.RemainingFuel();
            thrust = powerPlant.GetCurrentThrustN();

            CalculateAcceleration();
            CalculateVelocity();
            CalculateLift();
            CalculateBank();
            CalculateDrag();
            CalculateVerticalAcceleration();
            CalculateClimbRate(delta);
            CalculateAltitude(delta);
            CheckAutopilot();
        }

        private void CalculateBank()
        {
            if (BankAngle < 0)
            {
                Heading -= GetTurnRate();
            }
            else if (BankAngle > 0)
            {
                Heading += GetTurnRate();
            }
        }

        private double GetTurnRate()
        {
            double centripetalAcceleration = lateralForce / Mass;

            double turnRadius = velocity * velocity / centripetalAcceleration;

            double circumference = Math.PI * turnRadius * 2;

            double distanceTravelled = delta.TotalMilliseconds / 1000 * velocity;

            double degreesChanged = distanceTravelled / circumference * 360;

            return degreesChanged;
        }

        private void CheckAutopilot()
        {
            // AP Master must be on for anything to work
            if (cbApMASTER.Checked == false) return;

            // Requested Vertical Speed
            if (cbApVS.Checked)
            {
                int requestedClimbRate = 0;
                if (!int.TryParse(tbApVS.Text, out requestedClimbRate))
                {
                    requestedClimbRate = 0;
                    tbApVS.Text = "0";
                    cbApVS.Checked = false;
                    return;
                }

                RequestVerticalSpeed(requestedClimbRate);
            }

            // Requested Altitude
            if (cbApALT.Checked)
            {
                int requestedAltitude = 0;
                if (!int.TryParse(tbApALT.Text, out requestedAltitude))
                {
                    requestedAltitude = 0;
                    tbApVS.Text = "0";
                    cbApVS.Checked = false;
                    return;
                }

                RequestAltitude(requestedAltitude);
            }

            // Requested Heading
            if (cbApHDG.Checked)
            {
                int requestedHDG = 0;
                if (!int.TryParse(tbApHDG.Text, out requestedHDG))
                {
                    tbApHDG.Text = tbHDG.Text;
                    cbApHDG.Checked = false;
                    return;
                }

                RequestHeading(requestedHDG);
            }
        }

        private void RequestHeading(int requestedHDG)
        {
            double rate = 0.1;

            double diff = requestedHDG - Heading;

            if (diff > 180) BankAngle -= rate;
            else if (diff > 0 && diff <= 180) BankAngle += rate;
            else if (diff < 0 && diff >= -180) BankAngle += rate;
            else if (diff < -180) BankAngle -= rate;
        }

        private void RequestAltitude(int requestedAltitude)
        {
            int transientDifference = 200;

            double diff = requestedAltitude - altitude * METRES_TO_FEET;

            double req = 0;

            if (diff <= transientDifference && diff > 0) req = diff;
            else if (diff >= -transientDifference && diff < 0) req = diff;
            else if (diff > transientDifference)
                req = ApAltROC;
            else if (diff < -transientDifference)
                req = -ApAltROC;

            RequestVerticalSpeed(req);

            //Debug.WriteLine("Height Diff: " + diff + " Requesting V/S: " + req);
        }

        private void RequestVerticalSpeed(double requestedClimbRate)
        {
            // Work in FPM for convenience and divide by 1000 to get an arbitrary amount to make changed with
            // EG if we are climbing at 1000 fpm but need to climb at 1800 fpm then try to keep vertica
            double diff = (requestedClimbRate - (roc * METRES_PER_SECOND_TO_FEET_PER_MINUTE)) / 1000;

            // We need to go up
            if (diff > 0)
            {
                // So if we are accelerating downwards pull up
                if (vAcceleration <= 0) AoA += requestedClimbRate / 1000;
                else if (vAcceleration > 0)
                {
                    // We need to lower the nose if vAccel is too high
                    if (vAcceleration > diff || AoA > MAX_PITCH) AoA -= diff;
                    // And raise the nose if vAccel is too low
                    else if (vAcceleration < diff)
                        AoA += diff;
                }
            }
            else if (diff < 0) // we need to go down
            {
                // So if we are accelerating up, push forward
                if (vAcceleration >= 0)
                    AoA -= requestedClimbRate / 1000;
                else if (vAcceleration < 0)
                {
                    // We need to raise the nose if accelerating down too fast
                    if (vAcceleration < diff || AoA < MIN_PITCH) 
                        AoA -= diff;
                    // And lower the nose if vAccel is too high
                    else if (vAcceleration > diff) 
                        AoA += diff;
                }
            }

            Debug.WriteLine("V Accel: " + vAcceleration );
        }

        void UpdateSystems()
        {
            powerPlant.UpdateAllEngines(fuelSystem.BurnFuel, delta);
            powerPlant.RequestThrust(ThrustLever.Value);

            // If no fuel left then shut down
            if (remainingFuel < 1) powerPlant.ShutDownAll();
        }

        void UpdateDisplay()
        {
            int aoa = (int)AoA;

            tbFuel.Text = ((int)(remainingFuel)).ToString() + " kgs";
            ThrustPositionLabel.Text = "Thrust Lever Position (" + ThrustLever.Value + "%)";
            tbThrust.Text = (int)(powerPlant.GetCurrentThrustN()) + " N (" + powerPlant.GetCurrentThrustP() + "%)";
            tbTotal.Text = ((int)Mass).ToString() + " kgs";
            tbAcceleration.Text = ((int)acceleration).ToString() + " m/s/s (" + (acceleration/GRAVITATIONAL_CONSTANT).ToString("0.##") + "g)" ;
            tbLift.Text = ((int)downwardsLift).ToString() + " N";
            tbTotalDrag.Text = ((int)TotalDrag).ToString() + " N";
            tbTime.Text = ((int)((DateTime.Now - startRollOut).TotalSeconds)).ToString();
            tbAltitude.Text = ((int)(altitude * METRES_TO_FEET)).ToString() + " ft";
            tbLift.Text = ((int)downwardsLift).ToString() + " N";
            tbVAccel.Text = ((int)vAcceleration).ToString() + " m/s/s";
            tbRoc.Text = ((int)(roc * METRES_PER_SECOND_TO_FEET_PER_MINUTE)).ToString() + " fpm";
            labelAoAText.Text = aoa > 0 ? "AoA +" + aoa : (aoa < 0 ? "AoA " + aoa : "AoA 0");
            tbThrust1.Text = ((int)powerPlant.Engines[0].Thrust).ToString();
            tbThrust2.Text = ((int)powerPlant.Engines[1].Thrust).ToString();
            tbThrust3.Text = ((int)powerPlant.Engines[2].Thrust).ToString();
            tbThrust4.Text = ((int)powerPlant.Engines[3].Thrust).ToString();
            tbMain1.Text = ((int)fuelSystem.RemainingFuelIn("Main1")).ToString() + " kgs";
            tbMain2.Text = ((int)fuelSystem.RemainingFuelIn("Main2")).ToString() + " kgs";
            tbMain3.Text = ((int)fuelSystem.RemainingFuelIn("Main3")).ToString() + " kgs";
            tbMain4.Text = ((int)fuelSystem.RemainingFuelIn("Main4")).ToString() + " kgs";
            tbReserve2.Text = ((int)fuelSystem.RemainingFuelIn("Reserve2")).ToString() + " kgs";
            tbReserve3.Text = ((int)fuelSystem.RemainingFuelIn("Reserve3")).ToString() + " kgs";
            tbCenter.Text = ((int)fuelSystem.RemainingFuelIn("Center")).ToString() + " kgs";
            tbStabilizer.Text = ((int)fuelSystem.RemainingFuelIn("Stabilizer")).ToString() + " kgs";
            cbOn1.Checked = powerPlant.Engines[0].IsRunning;
            cbOn2.Checked = powerPlant.Engines[1].IsRunning;
            cbOn3.Checked = powerPlant.Engines[2].IsRunning;
            cbOn4.Checked = powerPlant.Engines[3].IsRunning;
            cbMain1.Checked = fuelSystem.IsValveOpen("Main1");
            cbMain2.Checked = fuelSystem.IsValveOpen("Main2");
            cbMain3.Checked = fuelSystem.IsValveOpen("Main3");
            cbMain4.Checked = fuelSystem.IsValveOpen("Main4");
            cbReserve2.Checked = fuelSystem.IsValveOpen("Reserve2");
            cbReserve3.Checked = fuelSystem.IsValveOpen("Reserve3");
            cbCenter.Checked = fuelSystem.IsValveOpen("Center");
            cbStabilizer.Checked = fuelSystem.IsValveOpen("Stabilizer");
            cbBrakes.Checked = isBrakeOn;
            tbRho.Text = Density.ToString("#.###") + " kg/m^2";
            tbOAT.Text = (Temperature + KELVIN_TO_CELSIUS).ToString("#.##") + " C";
            tbAirPressure.Text = Pressure.ToString("#") + " Pa";
            cbGear.Checked = landingGear;
            tbWindDirection.Text = ((int)windDirection).ToString("000");
            tbWindSpeed.Text = (windSpeed * METRES_PER_SECOND_TO_KNOTS).ToString() + " kts";
            tbHDG.Text = ((int)Heading).ToString("000");
            tbVelocity.Text = ((int)velocity * METRES_PER_SECOND_TO_KNOTS) + " kts";
            Ailerons.Value = (int)bankAngle;
            
            if (aoa <= 15 && aoa >= -15) Elevator.Value = aoa;
            if (aoa > 15) Elevator.Value = 15;
            if (aoa < -15) Elevator.Value = -15;
        }

        private void CalculateAcceleration()
        {
            if (isBrakeOn && altitude == 0) acceleration = 0;
            else acceleration = (((thrust * AltitudeThrustEfficiency) - TotalDrag - friction) / Mass);
        }

        private void CalculateVerticalAcceleration()
        {
            vAcceleration = (downwardsLift - (Mass * GRAVITATIONAL_CONSTANT)) / Mass;
            if (altitude == 0 && vAcceleration < 0) vAcceleration = 0;
        }

        private void CalculateClimbRate(TimeSpan delta)
        {
            roc += (vAcceleration * delta.TotalMilliseconds / 1000);
        }

        private void CalculateAltitude(TimeSpan delta)
        {
            double t = delta.TotalMilliseconds / 1000;

            altitude += (roc * t + 0.5 * vAcceleration * t * t);

            if (altitude < 0)
            {
                altitude = 0;
                roc = 0;
            }
        }

        private void CalculateVelocity()
        {
            velocity = velocity + (acceleration * delta.TotalMilliseconds / 1000);
        }

        void CalculateDrag()
        {
            double Cd = CD0 + Cdi * Cl * Cl;

            TotalDrag = 0.5 * Density * velocity * velocity * WING_AREA * Cd;
        }

        private void CalculateLift()
        {
            double totalLift = 0.5 * Density * velocity * velocity * WING_AREA * Cl;

            lateralForce = Math.Abs(BankAngle) / 90 * totalLift;

            downwardsLift = totalLift - lateralForce;
        }

        private void Pitch(object sender, EventArgs e)
        {
            AoA = Elevator.Value;
        }

        private void Ignition(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Engine engine = powerPlant.GetEngineFromEngineName(cb.Tag.ToString());

            if (cb.Checked) // turn the engine on
            {
                string fuelSource = "Offline";

                if (engine.Name == "Engine 1") fuelSource = cboSource1.SelectedItem.ToString();
                if (engine.Name == "Engine 2") fuelSource = cboSource2.SelectedItem.ToString();
                if (engine.Name == "Engine 3") fuelSource = cboSource3.SelectedItem.ToString();
                if (engine.Name == "Engine 4") fuelSource = cboSource4.SelectedItem.ToString();

                powerPlant.SetFuelSource(engine.Name, fuelSource);

                if (fuelSource == "Offline")
                {
                    MessageBox.Show("No tank online");
                    return;
                }

                // Check fuel is available
                if (!fuelSystem.IsFuelAvailable(fuelSource))
                {
                    MessageBox.Show("No fuel in tank or valve closed");
                    return;
                }

                powerPlant.SwitchOn(engine, fuelSystem);
            }
            else // turn the engine off
            {
                powerPlant.ShutDown(engine);
            }
        }

        private void FuelTankValve(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked)
            {
                fuelSystem.OpenFuelTankValve(cb.Tag.ToString());
            }
            else fuelSystem.CloseFuelTankValve(cb.Tag.ToString());
        }

        private void cbBrakes_CheckedChanged(object sender, EventArgs e)
        {
            SetBrake(cbBrakes.Checked);
        }

        private void SetBrake(bool isOn)
        {
            isBrakeOn = isOn;
            if (!isBrakeOn) startRollOut = DateTime.Now;
        }

        private void FuelSourceChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;

            string engineName = cbo.Tag.ToString();
            string requestedTankName = cbo.SelectedItem.ToString();

            if (fuelSystem.IsFuelAvailable(requestedTankName)) powerPlant.SetFuelSource(engineName, requestedTankName);
            else cbo.SelectedItem = powerPlant.GetFuelSource(engineName);
        }

        private void cbGear_CheckedChanged(object sender, EventArgs e)
        {
            landingGear = cbGear.Checked;
        }

        private void Aircraft_KeyDown(object sender, KeyEventArgs e)
        {
            double increment = 0.01;

            string key = "";

            if (e.KeyValue == 38) key = "u";
            if (e.KeyValue == 40) key = "d";
            switch (key)
            {
                case "u": AoA -= increment;
                    break;
                case "d": AoA += increment;
                    break;
            }
        }
    }
}
