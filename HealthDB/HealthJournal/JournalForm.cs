using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HealthJournal
{
    public partial class JournalForm : Form
    {
        HealthDataContext health;
        List<Route> routes;
        List<Activity> runs;
        List<Activity> walks;
        List<Stat> stats;

        public JournalForm()
        {
            InitializeComponent();
        }

        private void JournalForm_Load(object sender, EventArgs e)
        {
            health = new HealthDataContext();

            routes = (from r in health.Routes
                      select r).ToList();

            InitializeRunsList();
            InitializeStatsList();
            InitializeWalksList();

            PopulateRunsList();
            PopulateStatsList();
            PopulateWalksList();
        }

        private void InitializeStatsList()
        {
            listStats.View = View.Details;
            listStats.Columns.Add("Date", 70);
            listStats.Columns.Add("Day", 70);
            listStats.Columns.Add("Time", 55);
            listStats.Columns.Add("Kgs", 40);
            listStats.Columns.Add("Fat%", 40);
            listStats.Columns.Add("BMI", 40);
            listStats.Columns.Add("L Dia", 40);
            listStats.Columns.Add("L Sys", 40);
            listStats.Columns.Add("R Dia", 40);
            listStats.Columns.Add("R Sys", 40);
            listStats.Columns.Add("H/R", 40);
            listStats.Columns.Add("Notes", 300);
        }

        private void InitializeRunsList()
        {
            listRuns.View = View.Details;
            listRuns.Columns.Add("Date", 70);
            listRuns.Columns.Add("Day", 70);
            listRuns.Columns.Add("Time", 55);
            listRuns.Columns.Add("Route", 100);
            listRuns.Columns.Add("Time", 100);
            listRuns.Columns.Add("Notes", 300);
        }

        private void InitializeWalksList()
        {
            listWalks.View = View.Details;
            listWalks.Columns.Add("Date", 70);
            listWalks.Columns.Add("Day", 70);
            listWalks.Columns.Add("Time", 55);
            listWalks.Columns.Add("Route", 100);
            listWalks.Columns.Add("Time", 100);
            listWalks.Columns.Add("Notes", 300);
        }

        private void PopulateRunsList()
        {
            runs = (from r in health.Activities
                    where r.Walk == false
                    orderby r.Date descending, r.TimeOfDayID descending
                    select r).ToList();

            listRuns.Items.Clear();

            foreach (Activity run in runs)
            {
                ListViewItem item = new ListViewItem();
                item.Text = run.Date.ToString("d");
                item.SubItems.Add(run.Date.DayOfWeek.ToString());
                item.SubItems.Add((from t in health.TimeOfDays
                                   where t.ID == run.TimeOfDayID
                                   select t.Description).Single());
                item.SubItems.Add((from r in routes
                                   where r.ID == run.RouteID
                                   select r.Name).Single());
                item.SubItems.Add(run.Time.ToString());
                item.SubItems.Add(run.Notes);
                item.Tag = run.ID;
                listRuns.Items.Add(item);
            }
        }

        private void PopulateWalksList()
        {
            walks = (from w in health.Activities
                     where w.Walk == true
                     orderby w.Date descending, w.TimeOfDayID descending
                     select w).ToList();

            listWalks.Items.Clear();

            foreach (Activity walk in walks)
            {
                ListViewItem item = new ListViewItem();
                item.Text = walk.Date.ToString("d");
                item.SubItems.Add(walk.Date.DayOfWeek.ToString());
                item.SubItems.Add((from t in health.TimeOfDays
                                   where t.ID == walk.TimeOfDayID
                                   select t.Description).Single());
                item.SubItems.Add((from r in routes
                                   where r.ID == walk.RouteID
                                   select r.Name).Single());
                item.SubItems.Add(walk.Time.ToString());
                item.SubItems.Add(walk.Notes);
                item.Tag = walk.ID;
                listWalks.Items.Add(item);
            }
        }

        private void PopulateStatsList()
        {
            stats = (from s in health.Stats
                     orderby s.Date descending, s.TimeOfDayID descending
                     select s).ToList();

            listStats.Items.Clear();

            foreach (Stat stat in stats)
            {
                ListViewItem item = new ListViewItem();
                item.Text = stat.Date.ToString("d");
                item.SubItems.Add(stat.Date.DayOfWeek.ToString());
                item.SubItems.Add((from t in health.TimeOfDays
                                   where t.ID == stat.TimeOfDayID
                                   select t.Description).Single());
                item.SubItems.Add(stat.Weight == 0 ? "" : stat.Weight.ToString());
                item.SubItems.Add(stat.FatRatio == 0 ? "" : stat.FatRatio.ToString());
                item.SubItems.Add(stat.BMI == 0 ? "" : stat.BMI.ToString());
                item.SubItems.Add(stat.LDia.ToString());
                item.SubItems.Add(stat.LSys.ToString());
                item.SubItems.Add(stat.RDia.ToString());
                item.SubItems.Add(stat.RSys.ToString());
                item.SubItems.Add(stat.HeartRate.ToString());
                item.SubItems.Add(stat.Notes);
                item.Tag = stat.ID;
                listStats.Items.Add(item);
            }
        }

        private void NewRunEntry(object sender, EventArgs e)
        {
            NewRunForm form = new NewRunForm(0);
            form.ShowDialog();

            PopulateRunsList();
        }

        private void EditRunEntry(object sender, EventArgs e)
        {
            NewRunForm form = new NewRunForm((int)(listRuns.SelectedItems[0].Tag));
            form.ShowDialog();

            PopulateRunsList();
        }



        private void DeleteRunEntry(object sender, EventArgs e)
        {
            if (listRuns.SelectedItems.Count < 1) return;

            Activity run = (from r in health.Activities
                      where r.ID == (int)(listRuns.SelectedItems[0].Tag)
                      select r).Single();

            health.Activities.DeleteOnSubmit(run);
            health.SubmitChanges();

            PopulateRunsList();
        }

        private void NewStatEntry(object sender, EventArgs e)
        {
            NewStatForm form = new NewStatForm(0);
            form.ShowDialog();

            PopulateStatsList();
        }

        private void EditStatEntry(object sender, EventArgs e)
        {
            NewStatForm form = new NewStatForm((int)(listStats.SelectedItems[0].Tag));
            form.ShowDialog();

            PopulateStatsList();
        }

        private void DeleteStatEntry(object sender, EventArgs e)
        {
            if (listStats.SelectedItems.Count < 1) return;

            Stat stat = (from s in health.Stats
                       where s.ID == (int)(listStats.SelectedItems[0].Tag)
                       select s).Single();

            health.Stats.DeleteOnSubmit(stat);
            health.SubmitChanges();

            PopulateStatsList();
        }

        private void NewWalkEntry(object sender, EventArgs e)
        {
            NewWalkForm form = new NewWalkForm(0);
            form.ShowDialog();

            PopulateRunsList();
        }

        private void EditWalkEntry(object sender, EventArgs e)
        {
            NewWalkForm form = new NewWalkForm((int)(listWalks.SelectedItems[0].Tag));
            form.ShowDialog();

            PopulateRunsList();
        }

        private void DeleteWalkEntry(object sender, EventArgs e)
        {
            if (listWalks.SelectedItems.Count < 1) return;

            Activity walk = (from w in health.Activities
                            where w.ID == (int)(listWalks.SelectedItems[0].Tag)
                            select w).Single();

            health.Activities.DeleteOnSubmit(walk);
            health.SubmitChanges();

            PopulateRunsList();
        }

        private void EditRoutes(object sender, EventArgs e)
        {
            RoutesForm form = new RoutesForm();
            form.ShowDialog();
        }
    }
}
