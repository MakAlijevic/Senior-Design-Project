using AP_ShopBE.BLL.DTO;
using AP_ShopBE.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Interface
{
    public interface ICartItemService
    {
        Task<List<CartItem>> GetUserProducts(int id);

        Task<List<CartItem>> AddUserProduct(CartItemDto cartItem);

        Task<List<CartItem>> RemoveUserProduct(int id);

        Task<CartItem> UpdateUserProductQuantity(int id, int quantity);

        Task<List<CartItem>> BuyItems(int[] cartItems);

        Task<List<CartItem>> GetSelectedCartItems(int[] cartItems);

        Task<List<CartItem>> BuyNow(CartItemDto cartItem);

        Task DeleteAllCartItemsByProductId(int productId);
    }
}
