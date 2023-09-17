using AP_ShopBE.BLL.DTO;
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
    public class CartItemService : ICartItemService
    {
        private ICartItemRepository cartItemRepository;
        private IUserService userService;
        private IProductService productService;
        private IOrderService orderService;
        private IOrderProductService orderProductService;
        public CartItemService(ICartItemRepository cartItemRepository, IUserService userService, IProductService productService, IOrderService orderService, IOrderProductService orderProductService)
        {
            this.cartItemRepository = cartItemRepository;
            this.userService = userService;
            this.productService = productService;
            this.orderService = orderService;
            this.orderProductService = orderProductService;
        }

        public async Task<List<CartItem>> GetUserProducts(int id)
        {
            var userProducts = await cartItemRepository.GetUserProducts(id);

            List<CartItem> newUserProducts = new List<CartItem>();

            for(int i = 0; i < userProducts.Count; i++)
            {
                if (userProducts[i].Quantity > userProducts[i].Product.quantity)
                {
                    await UpdateUserProductQuantity(userProducts[i].Id, userProducts[i].Product.quantity);
                    userProducts[i].Quantity= userProducts[i].Product.quantity;
                    newUserProducts.Add(userProducts[i]);
                }
                else
                {
                    newUserProducts.Add(userProducts[i]);
                }
            }
            return newUserProducts;
        }
        public async Task<List<CartItem>> AddUserProduct(CartItemDto cartItem)
        {
            var userProducts = await GetUserProducts(cartItem.UserId);
            for(int i = 0; i< userProducts.Count; i++)
            {
                if (userProducts[i].ProductId == cartItem.ProductId)
                {
                    var product = await this.productService.GetProduct(cartItem.ProductId);
                    if ((userProducts[i].Quantity + cartItem.Quantity) > product.quantity)
                    {
                        //update to product.quantity
                        await this.cartItemRepository.UpdateProductQuantity(userProducts[i].Id, product.quantity);
                        return await this.cartItemRepository.GetUserProducts(cartItem.UserId);
                    }
                    else
                    {
                        //update to userProduct[i] + cartItem.Quantity
                        await this.cartItemRepository.UpdateProductQuantity(userProducts[i].Id, (userProducts[i].Quantity + cartItem.Quantity));
                        return await this.cartItemRepository.GetUserProducts(cartItem.UserId);
                    }
                }
            }
            var newCartItem = new CartItem
            {
                User = await this.userService.GetUser(cartItem.UserId),
                UserId = cartItem.UserId,
                Product = await this.productService.GetProduct(cartItem.ProductId),
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                dateTime = DateTime.Now,
            };
            //var user = await userService.GetUser(cartItem.UserId);
            await userService.UpdateUserCartModify(cartItem.UserId, false);

            return await cartItemRepository.AddUserProduct(newCartItem);
        }

        public async Task<List<CartItem>> RemoveUserProduct(int id)
        {
            var product = await this.cartItemRepository.GetCartItem(id);
            
            return await this.cartItemRepository.RemoveUserProduct(product);
        }

        public async Task<CartItem> UpdateUserProductQuantity(int id, int quantity)
        {
            var cartItem = await this.cartItemRepository.GetCartItem(id);
            var product = await this.productService.GetProduct(cartItem.ProductId);
            if(quantity <= product.quantity && quantity > 0)
            {
                return await this.cartItemRepository.UpdateProductQuantity(id, quantity);
            }
            throw new Exception("Invalid quantity");
            
        }

        public async Task<List<CartItem>> BuyItems(int[] cartItems)
        {
            var userId = cartItemRepository.GetCartItem(cartItems[0]).Result.UserId;

            for (int i = 0; i < cartItems.Length; i++)
            {
                var cartItem = await cartItemRepository.GetCartItem(cartItems[i]);

                var product = await productService.GetProduct(cartItem.ProductId);

                if (cartItem.Quantity > product.quantity)
                {
                    throw new Exception("Invalid quantity. Max quantity is " + product.quantity);
                }
            }

            //calculate total price
            double totalPrice = 0;
            for(int i = 0; i < cartItems.Length;i ++)
            {
                var cartItem = await cartItemRepository.GetCartItem(cartItems[i]);
                totalPrice += cartItem.Product.price * cartItem.Quantity;
            }

            //create order(CreateOrderDto(userId, totalPrice))
            var orderDTO = new CreateOrderDto{
                userId = userId,
                totalPrice= totalPrice
            };

            var order = await this.orderService.AddOrder(orderDTO);

            for(int i = 0; i < cartItems.Length; i++)
            {
                var cartItem = await cartItemRepository.GetCartItem(cartItems[i]);

                var product = await productService.GetProduct(cartItem.ProductId);

                //create new orderProduct (orderId, productId, quantity)
                var orderProduct = new OrderProductDto
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity
                };
                await this.orderProductService.AddOrderProduct(orderProduct);

                //decrease product quantity
                await this.productService.DecreaseQuantity(product.Id, cartItem.Quantity);

                //deleteCartItem(cartItemId)
                await this.cartItemRepository.RemoveUserProduct(cartItem);
            }
            return await this.cartItemRepository.GetUserProducts(userId);
        }

        public async Task<List<CartItem>> GetSelectedCartItems(int[] cartItems)
        {
            List<CartItem> newCartItems = new List<CartItem>();
            for(int i = 0; i < cartItems.Length; i++)
            {
                var cartItem = await this.cartItemRepository.GetCartItem(cartItems[i]);
                newCartItems.Add(cartItem);
            }
            return newCartItems;
        }

        public async Task<List<CartItem>> BuyNow(CartItemDto cartItem)
        {
            var product = await productService.GetProduct(cartItem.ProductId);

            //check input quantity
            if(cartItem.Quantity <= product.quantity && cartItem.Quantity > 0)
            {
                //calculate totalPrice
                var totalPrice = cartItem.Quantity * product.price;

                //create order
                var orderDTO = new CreateOrderDto
                {
                    userId = cartItem.UserId,
                    totalPrice = totalPrice
                };
                var order = await this.orderService.AddOrder(orderDTO);

                //create new orderProduct
                var orderProduct = new OrderProductDto
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity
                };
                await this.orderProductService.AddOrderProduct(orderProduct);

                //decrease product quantity
                await this.productService.DecreaseQuantity(product.Id, cartItem.Quantity);
            } else
            {
                throw new Exception("Invalid quantity!");
            }
            return await this.cartItemRepository.GetUserProducts(cartItem.UserId);
        }

        public async Task DeleteAllCartItemsByProductId(int productId)
        {
            await this.cartItemRepository.DeleteAllCartItemsByProductId(productId);
        }
    }
}
