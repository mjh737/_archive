using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HealthJournal
{
    public partial class NewStatForm : Form
    {
        HealthDataContext health;
        Stat stat;
        List<TimeOfDay> times;
        int statID;

        public NewStatForm(int statID)
        {
            InitializeComponent();

            this.statID = statID;
        }

        private void NewStatForm_Load(object sender, EventArgs e)
        {
            health = new HealthDataContext();

            if (statID == 0)
            {
                stat = new Stat();
                statDate.Value = (from r in health.Activities
                             select r.Date).Max() + TimeSpan.FromDays(1);
            }
            else stat = (from s in health.Stats
                        where s.ID == statID
                        select s).Single();

            times = (from t in health.TimeOfDays
                     select t).ToList();

            if (stat.Date >= statDate.MinDate && stat.Date <= statDate.MaxDate)
                statDate.Value = stat.Date;

            cboTimeOfDay.DataSource = times;
            cboTimeOfDay.DisplayMember = "Description";
            cboTimeOfDay.ValueMember = "ID";
            cboTimeOfDay.SelectedValue = stat.TimeOfDayID;

            tbWeight.Text = stat.Weight.ToString();
            tbFat.Text = stat.FatRatio.ToString();
            tbBMI.Text = stat.BMI.ToString();
            tbHR.Text = stat.HeartRate.ToString();
            tbNotes.Text = stat.Notes;

            tbLDia.Text = stat.LDia.ToString();
            tbRDia.Text = stat.RDia.ToString();
            tbLSys.Text = stat.LSys.ToString();
            tbRSys.Text = stat.RSys.ToString();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            stat.Date = statDate.Value;
            stat.TimeOfDayID = (int)cboTimeOfDay.SelectedValue;

            double weight;
            double.TryParse(tbWeight.Text, out weight);
            stat.Weight = weight;

            double fat;
            double.TryParse(tbFat.Text, out fat);
            stat.FatRatio = fat;

            double bmi;
            double.TryParse(tbBMI.Text, out bmi);
            stat.BMI = bmi;

            int rate;
            int.TryParse(tbHR.Text, out rate);
            stat.HeartRate = rate;

            stat.Notes = tbNotes.Text;

            int temp;
            int.TryParse(tbLDia.Text, out temp);
            stat.LDia = temp;
            temp = 0;
            int.TryParse(tbRDia.Text, out temp);
            stat.RDia = temp;
            temp = 0;
            int.TryParse(tbLSys.Text, out temp);
            stat.LSys = temp;
            temp = 0;
            int.TryParse(tbRSys.Text, out temp);
            stat.RSys = temp;

            if (stat.ID == 0) health.Stats.InsertOnSubmit(stat);

            health.SubmitChanges();

            this.Close();
        }
    }
}
