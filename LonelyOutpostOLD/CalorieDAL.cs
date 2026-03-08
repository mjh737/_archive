using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CalorieDAL
    {
        LonelyOutpostEntities ctx;

        public List<FoodItem> GetAllFoodItems()
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            return ctx.FoodItems.OrderBy(o => o.FoodName).ToList();
        }

        public List<string> GetFoodItemsInCategory(int categoryID)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            return ctx.FoodItems.Where(f => f.Category == categoryID).Select(f => f.FoodName).ToList();
        }

        public void InsertFoodItem(FoodItem item)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            ctx.FoodItems.AddObject(item);
            ctx.SaveChanges();
        }

        public List<FoodCategory> GetFoodCategories()
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            return ctx.FoodCategories.ToList();
        }

        public void AddCategory(string categoryName)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            if (categoryName == "") return;

            // If the category already exists, don't add it
            if (ctx.FoodCategories.Where(c => c.CategoryName == categoryName).Count() > 0) return;

            ctx.FoodCategories.AddObject(new FoodCategory() { CategoryName=categoryName });

            ctx.SaveChanges();
        }

        public void RemoveCategory(string categoryName)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            if (categoryName == "") return;

            // If the category doesn't exist, just return
            FoodCategory category = ctx.FoodCategories.Where(c => c.CategoryName == categoryName).SingleOrDefault();
            if (category == null) return;

            // Make sure all foods with this category are reset to category 0
            foreach (FoodItem item in ctx.FoodItems.Where(f => f.Category == category.CategoryID))
            {
                item.Category = 0;
            }

            ctx.FoodCategories.DeleteObject(category);

            ctx.SaveChanges();
        }

        public void RenameCategory(string oldCategoryName, string newCategoryName)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            if (oldCategoryName == "") return;
            if (newCategoryName == "") return;

            // If the new category already exists, don't add it
            if (ctx.FoodCategories.Where(c => c.CategoryName == newCategoryName).Count() > 0) return;

            // If the old category doesn't exist, just return
            FoodCategory category = ctx.FoodCategories.Where(c => c.CategoryName == oldCategoryName).SingleOrDefault();
            if (category == null) return;

            category.CategoryName = newCategoryName;

            ctx.SaveChanges();
        }

        public bool IsFoodInCategory(string foodName, int category)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            FoodItem item = ctx.FoodItems.Where(f => f.FoodName == foodName).Single();

            return (item.Category == category);
        }

        public void AddFoodItemToCategory(string foodName, int category)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            FoodItem item = ctx.FoodItems.Where(f => f.FoodName == foodName).Single();
            item.Category = category;
            ctx.SaveChanges();
        }

        public void RemoveFoodItemFromCategory(string foodItem)
        {
            if (ctx == null) ctx = new LonelyOutpostEntities();

            FoodItem itemToBeChanged = ctx.FoodItems.Where(f => f.FoodName == foodItem).SingleOrDefault();

            if (itemToBeChanged == null) return;

            itemToBeChanged.Category = 0;
            ctx.SaveChanges();
        }
    }
}
