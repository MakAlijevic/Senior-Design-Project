using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Interface
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetUserProducts(int id);

        Task<CartItem> GetCartItem(int id);

        Task<List<CartItem>> AddUserProduct(CartItem cartItem);

        Task<CartItem> UpdateProductQuantity(int id, int quantity);

        Task<List<CartItem>> RemoveUserProduct(CartItem cartItem);

        Task DeleteAllCartItemsByProductId(int productId);

        Task<List<CartItem>> GetAllCartItems();

        DataContext GetCartItemContext();
    }
}
