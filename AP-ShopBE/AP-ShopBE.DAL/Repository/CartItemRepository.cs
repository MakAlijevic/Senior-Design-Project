using AP_ShopBE.DAL.Data;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.DAL.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext context;
        public CartItemRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<CartItem>> GetUserProducts(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            var userProducts = await context.Cart_Item
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => x.User.Id == id)
                .ToListAsync();

            //if (!userProducts.Any())
            //{
            //    throw new Exception("User does not have items in cart");
            //}
            return userProducts;
        }

        public async Task<List<CartItem>> AddUserProduct(CartItem cartItem)
        {
            context.Cart_Item.Add(cartItem);
            await context.SaveChangesAsync();

            return await GetUserProducts(cartItem.UserId);
        }

        public async Task<List<CartItem>> RemoveUserProduct(CartItem cartItem)
        {
            context.Cart_Item.Remove(cartItem);
            await context.SaveChangesAsync();

            return await GetUserProducts(cartItem.User.Id);
        }

        public async Task<CartItem> GetCartItem(int id)
        {
            var cartItem = await context.Cart_Item
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return cartItem;
        }

        public async Task<CartItem> UpdateProductQuantity(int id, int quantity)
        {
            var cartItem = await GetCartItem(id);

            cartItem.Quantity= quantity;
            this.context.SaveChanges();

            return await GetCartItem(id);
        }

        public async Task DeleteAllCartItemsByProductId(int productId)
        {
            var cartItems = await context.Cart_Item.Where(x => x.ProductId== productId).ToListAsync();

            for(int i = 0; i< cartItems.Count; i++)
            {
                await RemoveUserProduct(cartItems[i]);
            }
        }

        public async Task<List<CartItem>> GetAllCartItems()
        {
            return await context.Cart_Item.ToListAsync();
        }

        public DataContext GetCartItemContext()
        {
            return context;
        }
    }
}
