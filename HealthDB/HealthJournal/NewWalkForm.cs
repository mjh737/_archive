using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HealthJournal
{
    public partial class NewWalkForm : Form
    {
        HealthDataContext health;
        Activity walk;
        List<Route> routes;
        List<TimeOfDay> times;

        public NewWalkForm(int walkID)
        {
            InitializeComponent();

            health = new HealthDataContext();

            if (walkID == 0)
            {
                walk = new Activity();
                WalkDate.Value = (from w in health.Activities
                                  select w.Date).Max() + TimeSpan.FromDays(1);
            }
            else walk = (from w in health.Activities
                        where w.ID == walkID
                        select w).Single();
        }

        private void LoadLookupLists()
        {
            routes = (from r in health.Routes
                      orderby r.ID
                      select r).ToList();

            times = (from t in health.TimeOfDays
                     select t).ToList();
        }

        private void NewWalkForm_Load(object sender, EventArgs e)
        {
            tbNotes.Text = walk.Notes;
            tbTime.Text = walk.Time.ToString();
            if (walk.Date >= WalkDate.MinDate && walk.Date <= WalkDate.MaxDate)
                WalkDate.Value = walk.Date;

            cboRoute.DataSource = routes;
            cboRoute.DisplayMember = "Name";
            cboRoute.ValueMember = "ID";
            cboRoute.SelectedValue = walk.RouteID;

            cboTimeOfDay.DataSource = times;
            cboTimeOfDay.DisplayMember = "Description";
            cboTimeOfDay.ValueMember = "ID";
            cboTimeOfDay.SelectedValue = walk.TimeOfDayID;
        }

        private void Save(object sender, EventArgs e)
        {
            walk.TimeOfDayID = (int)cboTimeOfDay.SelectedValue;
            walk.Date = WalkDate.Value;
            walk.Notes = tbNotes.Text;
            walk.RouteID = (int)cboRoute.SelectedValue;
            walk.Time = TimeSpan.Zero;
            walk.Walk = true;

            try
            {
                walk.Time = TimeSpan.Parse(tbTime.Text);
            }
            catch
            {
                walk.Time = TimeSpan.Zero;
            }

            if (walk.ID == 0) health.Activities.InsertOnSubmit(walk);

            // Check if beats personal best
            Route route = (from r in health.Routes
                           where r.ID == walk.RouteID
                           select r).Single();

            if (walk.Time < route.PersonalBest)
            {
                route.PersonalBest = walk.Time;
            }

            health.SubmitChanges();

            this.Close();
        }
    }
}
