using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HealthJournal
{
    public partial class RoutesForm : Form
    {
        HealthDataContext health;
        List<Route> routes;

        public RoutesForm()
        {
            InitializeComponent();

            LoadData();

            InitializeRoutesList();
        }

        private void LoadData()
        {
            health = new HealthDataContext();

            PopulateList();
        }

        private void InitializeRoutesList()
        {
            listRoutes.View = View.Details;
            listRoutes.Columns.Add("Name", 80);
            listRoutes.Columns.Add("Description", 400);
            listRoutes.Columns.Add("Miles", 40, HorizontalAlignment.Right);
            listRoutes.Columns.Add("Personal Best", 80, HorizontalAlignment.Right);
        }

        private void PopulateList()
        {
            routes = (from l in health.Routes
                      select l).ToList();

            listRoutes.Items.Clear();

            foreach (Route route in routes)
            {
                ListViewItem item = new ListViewItem();
                item.Text = route.Name;
                item.SubItems.Add(route.Description);
                item.SubItems.Add(route.Miles.ToString());
                item.SubItems.Add(route.PersonalBest.ToString());
                item.Tag = route.ID;
                listRoutes.Items.Add(item);
            }
        }

        private void NewRoute(object sender, EventArgs e)
        {
            RouteDetailsForm form = new RouteDetailsForm(0);
            form.ShowDialog();

            PopulateList();
        }

        private int GetSelectedRouteID()
        {
            if (listRoutes.SelectedItems.Count < 1) return 0;
            else return (int)listRoutes.SelectedItems[0].Tag;
        }

        private void DeleteRoute(object sender, EventArgs e)
        {
            if (listRoutes.SelectedItems.Count < 1) return;

            Route route = (from r in health.Routes
                           where r.ID == (int)(listRoutes.SelectedItems[0].Tag)
                           select r).Single();

            health.Routes.DeleteOnSubmit(route);
            health.SubmitChanges();

            PopulateList();
        }

        private void EditRoute(object sender, EventArgs e)
        {
            RouteDetailsForm form = new RouteDetailsForm(GetSelectedRouteID());
            form.ShowDialog();

            PopulateList();
        }

        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
