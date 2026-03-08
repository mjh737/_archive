using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lonelyOutpost
{
    public partial class P : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath(@"~/System/"));
            foreach (FileInfo f in dir.GetFiles("*.jpg"))
            {
                PlaceHolder1.Controls.Add(new Literal() { Text = "<br /><br />" });
                Image image = new Image();
                image.ImageUrl = @"~/System/" + Path.GetFileName(f.Name);
                PlaceHolder1.Controls.Add(image);

                HyperLink link = new HyperLink();
                PlaceHolder1.Controls.Add(new Literal() { Text = "<br /><br />" });
                link.NavigateUrl = @"~/System/" + Path.GetFileName(f.Name);
                link.Text = f.Name;
                PlaceHolder1.Controls.Add(link);
            }
        }
    }
}