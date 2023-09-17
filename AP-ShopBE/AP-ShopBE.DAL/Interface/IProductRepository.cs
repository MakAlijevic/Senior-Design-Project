using AP_ShopBE.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetFullSearchProducts(int[] categories, string searchParam, int minPrice, int maxPrice, int sortingType, int pageNumber);
        Task<List<Product>> GetSearchProducts(string searchParams);
        Task<Product> AddProduct(Product newProduct);
        Task<Product> UpdateProduct(Product newProduct, int id);
        Task<List<Product>> DeleteProduct(Product product);
        Task<Product> DecreaseQuantity(Product product, int quantity);
        Task<Product> UpdateIsModified(int id);
        Task<int> GetProductCount(int[] categories, string searchParam, int minPrice, int maxPrice, int sortingType, int pageNumber);
    }
}
