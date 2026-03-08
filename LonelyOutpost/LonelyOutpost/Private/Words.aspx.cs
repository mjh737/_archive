
namespace lonelyOutpost.Private
{
    public partial class Words : System.Web.UI.Page
    {
        protected string GetWords()
        {
            LonelyOutpostDAL dal = new LonelyOutpostDAL();

            return dal.GetNumberOfUnsortedWords().ToString();
        }
    }
}