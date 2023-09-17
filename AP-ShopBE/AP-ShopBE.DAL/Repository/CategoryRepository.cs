using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;
        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category doesn't exist");
            }
            return category;
        }

        public async Task<List<Category>> GetChildCategories(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category doesn't exist");
            }
            var categories = await context.Categories
                .Where(x => x.parentId == id)
                .ToListAsync();
            if (!categories.Any())
            {
                throw new Exception("This category doesn't have child categories");
            }
            return categories;
        }

        public async Task<Category> GetParentCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Category doesn't exist");
            }
            if (category.parentId == null)
            {
                throw new Exception("Category doesn't have parent category");
            }
            return await context.Categories.FindAsync(category.parentId);
        }
    }
}
