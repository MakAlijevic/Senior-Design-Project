﻿using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<List<Category>> GetChildCategories(int id);
        Task<Category> GetParentCategory(int id);
    }
}
