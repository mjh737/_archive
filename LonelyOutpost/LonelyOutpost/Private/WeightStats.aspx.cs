using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lonelyOutpost.Private
{
    public partial class WeightStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetDay(object dayNumber)
        {
            LonelyOutpostDAL dal = new LonelyOutpostDAL();

            return dal.GetDayFromDaysAlive(int.Parse(dayNumber.ToString())).ToShortDateString();
        }
    }
}