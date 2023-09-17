using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await categoryRepository.GetCategories();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await categoryRepository.GetCategory(id);
        }

        public async Task<List<Category>> GetChildCategories(int id)
        {
            return await categoryRepository.GetChildCategories(id);
        }

        public Task<Category> GetParentCategory(int id)
        {
            return categoryRepository.GetParentCategory(id);
        }
    }
}
