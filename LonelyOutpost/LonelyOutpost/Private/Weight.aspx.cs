using System;

namespace lonelyOutpost.Private
{
    public partial class Weight : System.Web.UI.Page
    {
        LonelyOutpostDAL dal;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void WeightButton_Click(object sender, EventArgs e)
        {
            TimeSpan time;
            if (!TimeSpan.TryParse(TimeList.SelectedValue, out time)) return;

            float weight;
            if (!float.TryParse(WeightTextBox.Text, out weight)) return;

            if (dal == null) dal = new LonelyOutpostDAL();

            dal.AddWeight(weight, time);

            NotificationLabel.Visible = true;
        }
    }
}