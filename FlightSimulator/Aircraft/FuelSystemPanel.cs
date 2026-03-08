using System.Drawing;
using System.Windows.Forms;

namespace FlightSimulator
{
    public partial class FuelSystemPanel : Form
    {
        FuelSystem fuelSystem;

        public FuelSystemPanel(FuelSystem fuelSystem)
        {
            InitializeComponent();

            this.fuelSystem = fuelSystem;

            int y = 30;

            foreach (Tank tank in fuelSystem.Tanks)
            {
                CheckBox cb = new CheckBox();
                cb.Name = tank.Name + "Valve";
                cb.Tag = tank.Name;
                cb.Checked = tank.isOpen;
                cb.Text = tank.Name + " Valve";
                cb.Location = new Point(10, y);
                cb.CheckedChanged += new System.EventHandler(cb_CheckedChanged);
                Controls.Add(cb);

                TextBox tb1 = new TextBox();
                tb1.Name = "tb" + tank.Name;
                
                tb1.Location = new Point(150, y);
                Controls.Add(tb1);
                tb1.Text = ((int)tank.Contents).ToString();

                TextBox tb2 = new TextBox();
                tb2.Name = "tb" + tank.Capacity;

                tb2.Location = new Point(300, y);
                Controls.Add(tb2);
                tb2.Text = ((int)tank.Capacity).ToString();

                y += 30;

                
            }
        }

        void cb_CheckedChanged(object sender, System.EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Checked) fuelSystem.OpenFuelTankValve(cb.Tag.ToString());
            else fuelSystem.CloseFuelTankValve(cb.Tag.ToString());
        }
    }
}
