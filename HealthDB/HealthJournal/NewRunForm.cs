using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HealthJournal
{
    public partial class NewRunForm : Form
    {
        HealthDataContext health;
        Activity run;
        List<Route> routes;
        List<TimeOfDay> times;

        public NewRunForm(int runID)
        {
            InitializeComponent();

            health = new HealthDataContext();

            if (runID == 0)
            {
                run = new Activity();
                RunDate.Value = (from r in health.Activities
                                 select r.Date).Max() + TimeSpan.FromDays(1);
            }
            else run = (from r in health.Activities
                        where r.ID == runID
                        select r).Single();

            LoadLookupLists();
            PopulateFields();
        }

        private void LoadLookupLists()
        {
            routes = (from r in health.Routes
                      orderby r.ID
                      select r).ToList();
            times = (from t in health.TimeOfDays
                     select t).ToList();
        }

        private void PopulateFields()
        {
            tbNotes.Text = run.Notes;
            tbTime.Text = run.Time.ToString();
            if (run.Date >= RunDate.MinDate && run.Date <= RunDate.MaxDate)
                RunDate.Value = run.Date;

            cboRoute.DataSource = routes;
            cboRoute.DisplayMember = "Name";
            cboRoute.ValueMember = "ID";
            cboRoute.SelectedValue = run.RouteID;

            cboTimeOfDay.DataSource = times;
            cboTimeOfDay.DisplayMember = "Description";
            cboTimeOfDay.ValueMember = "ID";
            cboTimeOfDay.SelectedValue = run.TimeOfDayID;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            run.TimeOfDayID = (int)cboTimeOfDay.SelectedValue;
            run.Date = RunDate.Value;
            run.Notes = tbNotes.Text;
            run.RouteID = (int)cboRoute.SelectedValue;
            run.Time = TimeSpan.Zero;
            try
            {
                run.Time = TimeSpan.Parse(tbTime.Text);
            }
            catch
            {
                run.Time = TimeSpan.Zero;
            }

            run.Walk = false;

            if (run.ID == 0) health.Activities.InsertOnSubmit(run);

            // Check if beats personal best
            Route route = (from r in health.Routes
                           where r.ID == run.RouteID
                           select r).Single();

            if (run.Time < route.PersonalBest)
            {
                route.PersonalBest = run.Time;
            }

            health.SubmitChanges();

            this.Close();
        }
    }
}
