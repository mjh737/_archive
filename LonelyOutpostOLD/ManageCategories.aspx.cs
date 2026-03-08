using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace LonelyOutpost.Private.Calories
{
    public partial class ManageCategories : System.Web.UI.Page
    {
        CalorieDAL dal = new CalorieDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DisplayCategoriesInGrid();
            }
        }

        protected void CreateCategoryButton_Click(object sender, EventArgs e)
        {
            string newCategoryName = CategoryName.Text.Trim();

            dal.AddCategory(newCategoryName);

            DisplayCategoriesInGrid();

            CategoryName.Text = "";
        }

        

        private void DisplayCategoriesInGrid()
        {
            CategoryList.DataSource = dal.GetFoodCategories();
            CategoryList.DataBind();
        }

        protected void CategoryList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the CategoryNameLabel
            Label CategoryNameLabel = CategoryList.Rows[e.RowIndex].FindControl("CategoryLabel") as Label;
            
            // Delete the role
            dal.RemoveCategory(CategoryNameLabel.Text);
            
            // Rebind the data to the CategoryList grid
            DisplayCategoriesInGrid();
        }
    }
}