using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace LonelyOutpost.Private.Calories
{
    public partial class Categories : System.Web.UI.Page
    {
        CalorieDAL dal = new CalorieDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCategoriesToList();
                BindFoodItems();
                CheckFoodItemsInSelectedCategory();
            }
        }

        private void BindFoodItems()
        {
            // Get all of the food items
            var foodItems = dal.GetAllFoodItems();

            FoodItemsList.DataSource = foodItems;
            FoodItemsList.DataBind();
        }

        private void BindCategoriesToList()
        {
            // Get all of the categories
            CategoryList.DataSource = dal.GetFoodCategories();
            CategoryList.DataBind();
        }

        protected void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFoodItemsInSelectedCategory();
        }

        protected void FoodItemCheckBox_CheckChanged(object sender, EventArgs e)
        {
            // Reference the CheckBox that raised this event
            CheckBox FoodCheckBox = sender as CheckBox;
            
            // Get the currently selected category and food item
            string selectedFoodItem = FoodCheckBox.Text;
            string categoryName =   CategoryList.SelectedItem.Text;
            
            // Determine if we need to add or remove the food from the category
            if (FoodCheckBox.Checked)
            {            
                // Add the user to the role
                dal.AddFoodItemToCategory(selectedFoodItem, int.Parse(CategoryList.SelectedValue));
                
                // Display a status message
                ActionStatus.Text = string.Format("Food Item {0} was added to Category {1}.", selectedFoodItem, categoryName);       
            }
            else
            {
                // Remove the food item from the role
                dal.RemoveFoodItemFromCategory(selectedFoodItem);
                // Display a status message
                ActionStatus.Text = string.Format("Food Item {0} was removed from Category {1}.", selectedFoodItem, categoryName);        
            }
        }

        private void CheckFoodItemsInSelectedCategory()
        {
            // Determine what roles the selected user belongs to
            int selectedcategory = int.Parse(CategoryList.SelectedValue);
            List<string> selectedFoodItems = dal.GetFoodItemsInCategory(selectedcategory);
            
            // Loop through the Repeater's Items and check or uncheck the checkbox as needed
            foreach (RepeaterItem ri in FoodItemsList.Items)
            {
                // Programmatically reference the CheckBox
                CheckBox FoodItemCheckBox = ri.FindControl("cbFoodItem") as CheckBox;
                
                // See if cbFoodItem.Text is in this category
                FoodItemCheckBox.Checked = selectedFoodItems.Contains(FoodItemCheckBox.Text);
            }
        }
    }
}