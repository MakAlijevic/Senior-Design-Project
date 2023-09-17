using AP_ShopBE.BLL.DTO;
using AP_ShopBE.BLL.Interface;
using AP_ShopBE.DAL.Interface;
using AP_ShopBE.DAL.Model;
using System.Threading;
using AP_ShopBE.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_ShopBE.BLL.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        private IConditionRepository conditionRepository;
        private IShippingRepository shippingRepository;
        private ICartItemRepository cartItemRepository;
        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IConditionRepository conditionRepository, IShippingRepository shippingRepository, ICartItemRepository cartItemRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.conditionRepository = conditionRepository;
            this.shippingRepository = shippingRepository;
            this.cartItemRepository = cartItemRepository;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await productRepository.GetProducts();
        }
        public async Task<List<Product>> GetProductsSorted(int sortingType)
        {
            var products = await productRepository.GetProducts();

            if (sortingType == 1)
            {
                return products.OrderBy(product => product.productName).ToList();
            }
            else if (sortingType == 2)
            {
                return products.OrderByDescending(product => product.productName).ToList();
            }
            else if (sortingType == 3)
            {
                return products.OrderBy(product => product.price).ToList();
            }
            else if (sortingType == 4)
            {
                return products.OrderByDescending(product => product.price).ToList();
            }

            return products;

        }

        public async Task<Product> GetProduct(int id)
        {
            return await productRepository.GetProduct(id);
        }

        public Task<List<Product>> GetFullSearchProducts(SearchDto searchCriteria)
        {
            return productRepository.GetFullSearchProducts(searchCriteria.categories, searchCriteria.searchParam, searchCriteria.minPrice, searchCriteria.maxPrice, searchCriteria.sortingType, searchCriteria.pageNumber);
        }

        public async Task<List<Product>> GetSearchProducts(string searchParams, int sortingType)
        {

            var products =  await productRepository.GetSearchProducts(searchParams);

            if(sortingType == 1)
            {
                return products.OrderBy(product => product.productName).ToList();
            } 
            else if(sortingType == 2)
            {
                return products.OrderByDescending(product => product.productName).ToList();
            }
            else if (sortingType == 3)
            {
                return products.OrderBy(product => product.price).ToList();
            }
            else if (sortingType == 4)
            {
                return products.OrderByDescending(product => product.price).ToList();
            }

            return products;
        }

        public async Task<Product> AddProduct(CreateProductDto request)
        {
            var newProduct = new Product
            {
                productName = request.productName,
                imagePath = request.imagePath,
                price = request.price,
                description = request.description,
                isDeleted = false,
                quantity = request.quantity,
                Category = await categoryRepository.GetCategory(request.categoryId),
                Condition = await conditionRepository.GetCondition(request.conditionId),
                Shipping = await shippingRepository.GetShipping(request.shippingId),
                isModified = false
            };

            return await productRepository.AddProduct(newProduct);
        }

        public async Task<Product> UpdateProduct(CreateProductDto request, int id)
        {
            var newProduct = new Product
            {
                productName = request.productName,
                imagePath = request.imagePath,
                price = request.price,
                description = request.description,
                isDeleted = false,
                quantity = request.quantity,
                Category = await categoryRepository.GetCategory(request.categoryId),
                categoryId= request.categoryId,
                Condition = await conditionRepository.GetCondition(request.conditionId),
                conditionId = request.conditionId,
                Shipping = await shippingRepository.GetShipping(request.shippingId),
                shippingId= request.shippingId
            };

            return await productRepository.UpdateProduct(newProduct, id);
        }   

        public async Task<List<Product>> DeleteProduct(int id) 
        {
            var product = await GetProduct(id);
            await cartItemRepository.DeleteAllCartItemsByProductId(id);

            return await productRepository.DeleteProduct(product);
        }

        public async Task<Product> DecreaseQuantity(int productId, int quantity)
        {
            var product = await GetProduct(productId);

            return await this.productRepository.DecreaseQuantity(product, quantity);

        }

        public async Task<Product> UpdateIsModified(int id)
        {
            return await this.productRepository.UpdateIsModified(id);
        }

        public async Task<int> GetProductCount(SearchDto searchCriteria)
        {
            return await productRepository.GetProductCount(searchCriteria.categories, searchCriteria.searchParam, searchCriteria.minPrice, searchCriteria.maxPrice, searchCriteria.sortingType, searchCriteria.pageNumber);

        }
    }
}
