using System.Drawing;
using System.Windows.Forms;

namespace FlightSimulator
{
    public partial class PowerPlantPanel : Form
    {
        PowerPlant powerPlant;
        FuelSystem fuelSystem;

        public string Message { get; set; }

        public PowerPlantPanel(PowerPlant powerPlant, FuelSystem fuelSystem)
        {
            InitializeComponent();

            this.powerPlant = powerPlant;
            this.fuelSystem = fuelSystem;

            int y = 30;

            foreach (Engine engine in powerPlant.Engines)
            {
                CheckBox cb = new CheckBox();
                cb.Name = "Engine" + engine.Number;
                cb.Tag = engine.Number;
                cb.Checked = engine.isOn;
                cb.Text = "Engine " + engine.Number;
                cb.Location = new Point(10, y);
                cb.CheckedChanged += new System.EventHandler(cb_CheckedChanged);
                Controls.Add(cb);

                //ComboBox cbo = new ComboBox();
                //cbo.Name = "cbo" + engine.TankFeed;
                //cbo.DataSource = fuelSystem.Tanks;
                //cbo.ValueMember = cbo.DisplayMember = "Name";
                //cbo.SelectedValue = engine.TankFeed;
                //cbo.Location = new Point(150, y);
                //Controls.Add(cbo);

                y += 30;
            }
        }

        void cb_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Checked) // turn the engine on
            {
                // Check fuel is available
                if (!fuelSystem.IsFuelAvailable)
                {
                    Message = "No Fuel Available";
                    return;
                }

                powerPlant.SwitchOn(cb.Tag.ToString());
            }
            else // turn the engine off
            {
                
            }


        }
    }
}
