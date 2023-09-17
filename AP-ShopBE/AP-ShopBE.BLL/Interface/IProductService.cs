using AP_ShopBE.BLL.DTO;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductsSorted(int sortingType);
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetSearchProducts(string searchParams, int sortingType);
        Task<List<Product>> GetFullSearchProducts(SearchDto searchCriteria);
        Task<int> GetProductCount(SearchDto searchCriteria);
        Task<Product> AddProduct(CreateProductDto request);
        Task<Product> UpdateProduct(CreateProductDto request, int id);
        Task<List<Product>> DeleteProduct(int id);
        Task<Product> DecreaseQuantity(int productId, int quantity);
        Task<Product> UpdateIsModified(int id);
    }
}
