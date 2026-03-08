namespace B737
{
    partial class Cockpit
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            EnvironmentTab = new TabPage();
            txtAmbientPressure = new TextBox();
            txtAmbientTemperature = new TextBox();
            txtSpeedOfSound = new TextBox();
            ElectricsTab = new TabPage();
            MainBatteryOnlineCheckBox = new CheckBox();
            BatteryVoltageLabel = new Label();
            PowerTab = new TabPage();
            NetForceLabel = new Label();
            AccelerationLabel = new Label();
            MassLabel = new Label();
            RightFuelCutoffCheckBox = new CheckBox();
            LeftFuelCutoffCheckBox = new CheckBox();
            RightFuelFlowLabel = new Label();
            LeftFuelFlowLabel = new Label();
            RightN1Label = new Label();
            LeftN1Label = new Label();
            RightThrustLabel = new Label();
            LeftThrustLabel = new Label();
            RightThrottleValueLabel = new Label();
            LeftThrottleValueLabel = new Label();
            RightThrottleSlider = new TrackBar();
            LeftThrottleSlider = new TrackBar();
            BothThrottleValueLabel = new Label();
            BothThrottleSlider = new TrackBar();
            FuelLabel = new Label();
            Tabs = new TabControl();
            EnvironmentTab.SuspendLayout();
            ElectricsTab.SuspendLayout();
            PowerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RightThrottleSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LeftThrottleSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BothThrottleSlider).BeginInit();
            Tabs.SuspendLayout();
            SuspendLayout();
            // 
            // EnvironmentTab
            // 
            EnvironmentTab.Controls.Add(txtAmbientPressure);
            EnvironmentTab.Controls.Add(txtAmbientTemperature);
            EnvironmentTab.Controls.Add(txtSpeedOfSound);
            EnvironmentTab.Location = new Point(4, 29);
            EnvironmentTab.Name = "EnvironmentTab";
            EnvironmentTab.Padding = new Padding(3);
            EnvironmentTab.Size = new Size(766, 629);
            EnvironmentTab.TabIndex = 2;
            EnvironmentTab.Text = "Environment";
            EnvironmentTab.UseVisualStyleBackColor = true;
            // 
            // txtAmbientPressure
            // 
            txtAmbientPressure.Location = new Point(308, 261);
            txtAmbientPressure.Name = "txtAmbientPressure";
            txtAmbientPressure.ReadOnly = true;
            txtAmbientPressure.Size = new Size(150, 27);
            txtAmbientPressure.TabIndex = 6;
            // 
            // txtAmbientTemperature
            // 
            txtAmbientTemperature.Location = new Point(308, 301);
            txtAmbientTemperature.Name = "txtAmbientTemperature";
            txtAmbientTemperature.ReadOnly = true;
            txtAmbientTemperature.Size = new Size(150, 27);
            txtAmbientTemperature.TabIndex = 7;
            // 
            // txtSpeedOfSound
            // 
            txtSpeedOfSound.Location = new Point(308, 341);
            txtSpeedOfSound.Name = "txtSpeedOfSound";
            txtSpeedOfSound.ReadOnly = true;
            txtSpeedOfSound.Size = new Size(150, 27);
            txtSpeedOfSound.TabIndex = 8;
            // 
            // ElectricsTab
            // 
            ElectricsTab.Controls.Add(MainBatteryOnlineCheckBox);
            ElectricsTab.Controls.Add(BatteryVoltageLabel);
            ElectricsTab.Location = new Point(4, 29);
            ElectricsTab.Name = "ElectricsTab";
            ElectricsTab.Padding = new Padding(3);
            ElectricsTab.Size = new Size(766, 629);
            ElectricsTab.TabIndex = 1;
            ElectricsTab.Text = "Electrics";
            ElectricsTab.UseVisualStyleBackColor = true;
            // 
            // MainBatteryOnlineCheckBox
            // 
            MainBatteryOnlineCheckBox.AutoSize = true;
            MainBatteryOnlineCheckBox.Location = new Point(18, 18);
            MainBatteryOnlineCheckBox.Name = "MainBatteryOnlineCheckBox";
            MainBatteryOnlineCheckBox.Size = new Size(162, 24);
            MainBatteryOnlineCheckBox.TabIndex = 0;
            MainBatteryOnlineCheckBox.Text = "Main Battery Online";
            MainBatteryOnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // BatteryVoltageLabel
            // 
            BatteryVoltageLabel.AutoSize = true;
            BatteryVoltageLabel.Location = new Point(18, 52);
            BatteryVoltageLabel.Name = "BatteryVoltageLabel";
            BatteryVoltageLabel.Size = new Size(143, 20);
            BatteryVoltageLabel.TabIndex = 1;
            BatteryVoltageLabel.Text = "Battery Voltage: -- V";
            // 
            // PowerTab
            // 
            PowerTab.Controls.Add(NetForceLabel);
            PowerTab.Controls.Add(AccelerationLabel);
            PowerTab.Controls.Add(MassLabel);
            PowerTab.Controls.Add(RightFuelCutoffCheckBox);
            PowerTab.Controls.Add(LeftFuelCutoffCheckBox);
            PowerTab.Controls.Add(RightFuelFlowLabel);
            PowerTab.Controls.Add(LeftFuelFlowLabel);
            PowerTab.Controls.Add(RightN1Label);
            PowerTab.Controls.Add(LeftN1Label);
            PowerTab.Controls.Add(RightThrustLabel);
            PowerTab.Controls.Add(LeftThrustLabel);
            PowerTab.Controls.Add(RightThrottleValueLabel);
            PowerTab.Controls.Add(LeftThrottleValueLabel);
            PowerTab.Controls.Add(RightThrottleSlider);
            PowerTab.Controls.Add(LeftThrottleSlider);
            PowerTab.Controls.Add(BothThrottleValueLabel);
            PowerTab.Controls.Add(BothThrottleSlider);
            PowerTab.Controls.Add(FuelLabel);
            PowerTab.Location = new Point(4, 29);
            PowerTab.Name = "PowerTab";
            PowerTab.Padding = new Padding(3);
            PowerTab.Size = new Size(766, 629);
            PowerTab.TabIndex = 0;
            PowerTab.Text = "Power";
            PowerTab.UseVisualStyleBackColor = true;
            // 
            // NetForceLabel
            // 
            NetForceLabel.AutoSize = true;
            NetForceLabel.Location = new Point(15, 189);
            NetForceLabel.Name = "NetForceLabel";
            NetForceLabel.Size = new Size(158, 20);
            NetForceLabel.TabIndex = 26;
            NetForceLabel.Text = "Net Force Magnitude: -- N";
            // 
            // AccelerationLabel
            // 
            AccelerationLabel.AutoSize = true;
            AccelerationLabel.Location = new Point(15, 209);
            AccelerationLabel.Name = "AccelerationLabel";
            AccelerationLabel.Size = new Size(177, 20);
            AccelerationLabel.TabIndex = 27;
            AccelerationLabel.Text = "Acceleration: -- m/s²";
            // 
            // MassLabel
            // 
            MassLabel.AutoSize = true;
            MassLabel.Location = new Point(15, 229);
            MassLabel.Name = "MassLabel";
            MassLabel.Size = new Size(115, 20);
            MassLabel.TabIndex = 28;
            MassLabel.Text = "Mass: -- kg";
            // 
            // RightFuelCutoffCheckBox
            // 
            RightFuelCutoffCheckBox.AutoSize = true;
            RightFuelCutoffCheckBox.Location = new Point(325, 378);
            RightFuelCutoffCheckBox.Name = "RightFuelCutoffCheckBox";
            RightFuelCutoffCheckBox.Size = new Size(142, 24);
            RightFuelCutoffCheckBox.TabIndex = 25;
            RightFuelCutoffCheckBox.Text = "Right Fuel Cutoff";
            RightFuelCutoffCheckBox.UseVisualStyleBackColor = true;
            // 
            // LeftFuelCutoffCheckBox
            // 
            LeftFuelCutoffCheckBox.AutoSize = true;
            LeftFuelCutoffCheckBox.Location = new Point(325, 328);
            LeftFuelCutoffCheckBox.Name = "LeftFuelCutoffCheckBox";
            LeftFuelCutoffCheckBox.Size = new Size(132, 24);
            LeftFuelCutoffCheckBox.TabIndex = 24;
            LeftFuelCutoffCheckBox.Text = "Left Fuel Cutoff";
            LeftFuelCutoffCheckBox.UseVisualStyleBackColor = true;
            // 
            // RightFuelFlowLabel
            // 
            RightFuelFlowLabel.AutoSize = true;
            RightFuelFlowLabel.Location = new Point(15, 398);
            RightFuelFlowLabel.Name = "RightFuelFlowLabel";
            RightFuelFlowLabel.Size = new Size(159, 20);
            RightFuelFlowLabel.TabIndex = 23;
            RightFuelFlowLabel.Text = "Right Fuel Flow: 0 kg/h";
            // 
            // LeftFuelFlowLabel
            // 
            LeftFuelFlowLabel.AutoSize = true;
            LeftFuelFlowLabel.Location = new Point(15, 348);
            LeftFuelFlowLabel.Name = "LeftFuelFlowLabel";
            LeftFuelFlowLabel.Size = new Size(149, 20);
            LeftFuelFlowLabel.TabIndex = 22;
            LeftFuelFlowLabel.Text = "Left Fuel Flow: 0 kg/h";
            // 
            // RightN1Label
            // 
            RightN1Label.AutoSize = true;
            RightN1Label.Location = new Point(15, 378);
            RightN1Label.Name = "RightN1Label";
            RightN1Label.Size = new Size(94, 20);
            RightN1Label.TabIndex = 21;
            RightN1Label.Text = "Right N1: 0%";
            // 
            // LeftN1Label
            // 
            LeftN1Label.AutoSize = true;
            LeftN1Label.Location = new Point(15, 328);
            LeftN1Label.Name = "LeftN1Label";
            LeftN1Label.Size = new Size(84, 20);
            LeftN1Label.TabIndex = 20;
            LeftN1Label.Text = "Left N1: 0%";
            // 
            // RightThrustLabel
            // 
            RightThrustLabel.AutoSize = true;
            RightThrustLabel.Location = new Point(15, 298);
            RightThrustLabel.Name = "RightThrustLabel";
            RightThrustLabel.Size = new Size(103, 20);
            RightThrustLabel.TabIndex = 19;
            RightThrustLabel.Text = "Right Thrust: 0";
            // 
            // LeftThrustLabel
            // 
            LeftThrustLabel.AutoSize = true;
            LeftThrustLabel.Location = new Point(15, 268);
            LeftThrustLabel.Name = "LeftThrustLabel";
            LeftThrustLabel.Size = new Size(93, 20);
            LeftThrustLabel.TabIndex = 18;
            LeftThrustLabel.Text = "Left Thrust: 0";
            // 
            // RightThrottleValueLabel
            // 
            RightThrottleValueLabel.AutoSize = true;
            RightThrottleValueLabel.Location = new Point(325, 161);
            RightThrottleValueLabel.Name = "RightThrottleValueLabel";
            RightThrottleValueLabel.Size = new Size(115, 20);
            RightThrottleValueLabel.TabIndex = 17;
            RightThrottleValueLabel.Text = "Right Throttle: 0";
            // 
            // LeftThrottleValueLabel
            // 
            LeftThrottleValueLabel.AutoSize = true;
            LeftThrottleValueLabel.Location = new Point(325, 81);
            LeftThrottleValueLabel.Name = "LeftThrottleValueLabel";
            LeftThrottleValueLabel.Size = new Size(105, 20);
            LeftThrottleValueLabel.TabIndex = 16;
            LeftThrottleValueLabel.Text = "Left Throttle: 0";
            // 
            // RightThrottleSlider
            // 
            RightThrottleSlider.Location = new Point(15, 149);
            RightThrottleSlider.Maximum = 100;
            RightThrottleSlider.Name = "RightThrottleSlider";
            RightThrottleSlider.Size = new Size(300, 56);
            RightThrottleSlider.TabIndex = 15;
            RightThrottleSlider.TickFrequency = 10;
            // 
            // LeftThrottleSlider
            // 
            LeftThrottleSlider.Location = new Point(15, 69);
            LeftThrottleSlider.Maximum = 100;
            LeftThrottleSlider.Name = "LeftThrottleSlider";
            LeftThrottleSlider.Size = new Size(300, 56);
            LeftThrottleSlider.TabIndex = 14;
            LeftThrottleSlider.TickFrequency = 10;
            // 
            // BothThrottleValueLabel
            // 
            BothThrottleValueLabel.AutoSize = true;
            BothThrottleValueLabel.Location = new Point(325, 121);
            BothThrottleValueLabel.Name = "BothThrottleValueLabel";
            BothThrottleValueLabel.Size = new Size(111, 20);
            BothThrottleValueLabel.TabIndex = 16;
            BothThrottleValueLabel.Text = "Both Throttle: 0";
            // 
            // BothThrottleSlider
            // 
            BothThrottleSlider.BackColor = Color.LightCoral;
            BothThrottleSlider.Location = new Point(15, 109);
            BothThrottleSlider.Maximum = 100;
            BothThrottleSlider.Name = "BothThrottleSlider";
            BothThrottleSlider.Size = new Size(300, 56);
            BothThrottleSlider.TabIndex = 15;
            BothThrottleSlider.TickFrequency = 10;
            // 
            // FuelLabel
            // 
            FuelLabel.AutoSize = true;
            FuelLabel.Location = new Point(15, 19);
            FuelLabel.Name = "FuelLabel";
            FuelLabel.Size = new Size(111, 20);
            FuelLabel.TabIndex = 13;
            FuelLabel.Text = "Fuel Remaining";
            // 
            // Tabs
            // 
            Tabs.Controls.Add(PowerTab);
            Tabs.Controls.Add(ElectricsTab);
            Tabs.Controls.Add(EnvironmentTab);
            Tabs.Location = new Point(654, 12);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            Tabs.Size = new Size(774, 662);
            Tabs.TabIndex = 13;
            // 
            // Cockpit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1430, 686);
            Controls.Add(Tabs);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Cockpit";
            Text = "Boeing 737-800 NG";
            WindowState = FormWindowState.Maximized;
            EnvironmentTab.ResumeLayout(false);
            EnvironmentTab.PerformLayout();
            ElectricsTab.ResumeLayout(false);
            ElectricsTab.PerformLayout();
            PowerTab.ResumeLayout(false);
            PowerTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RightThrottleSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)LeftThrottleSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)BothThrottleSlider).EndInit();
            Tabs.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage EnvironmentTab;
        private TabPage ElectricsTab;
        private TabPage PowerTab;
        private CheckBox RightFuelCutoffCheckBox;
        private CheckBox LeftFuelCutoffCheckBox;
        private Label RightFuelFlowLabel;
        private Label LeftFuelFlowLabel;
        private Label RightN1Label;
        private Label LeftN1Label;
        private Label RightThrustLabel;
        private Label LeftThrustLabel;
        private Label RightThrottleValueLabel;
        private Label LeftThrottleValueLabel;
        private Label BothThrottleValueLabel;
        private TrackBar RightThrottleSlider;
        private TrackBar LeftThrottleSlider;
        private TrackBar BothThrottleSlider;
        private Label FuelLabel;
        private TabControl Tabs;
        private TextBox txtAmbientPressure;
        private TextBox txtAmbientTemperature;
        private TextBox txtSpeedOfSound;
        private CheckBox MainBatteryOnlineCheckBox;
        private Label BatteryVoltageLabel;
        private Label NetForceLabel;
        private Label AccelerationLabel;
        private Label MassLabel;
    }
}
