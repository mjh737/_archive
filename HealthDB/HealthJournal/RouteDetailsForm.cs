using System;
using System.Linq;
using System.Windows.Forms;

namespace HealthJournal
{
    public partial class RouteDetailsForm : Form
    {
        int id;
        HealthDataContext ctx;
        Route route;

        public RouteDetailsForm(int routeID)
        {
            InitializeComponent();

            this.id = routeID;
        }

        private void RouteForm_Load(object sender, EventArgs e)
        {
            ctx = new HealthDataContext();

            LoadData();
        }

        private void LoadData()
        {
            if (id == 0) route = new Route();
            else route = (from r in ctx.Routes
                          where r.ID == id
                          select r).Single();

            tbName.Text = route.Name;
            tbDescription.Text = route.Description;
            tbDistance.Text = route.Miles.ToString();
            tbPersonalBest.Text = route.PersonalBest.ToString();
        }

        private void Save(object sender, EventArgs e)
        {
            route.Name = tbName.Text;
            route.Description = tbDescription.Text;
            
            int miles = 0;
            int.TryParse(tbDistance.Text, out miles);
            if (miles > 0) route.Miles = miles;

            TimeSpan time = TimeSpan.Zero;
            TimeSpan.TryParse(tbPersonalBest.Text, out time);
            if (time > TimeSpan.Zero) route.PersonalBest = time;

            if (id == 0) ctx.Routes.InsertOnSubmit(route);

            ctx.SubmitChanges();

            this.Close();
        }


    }
}
