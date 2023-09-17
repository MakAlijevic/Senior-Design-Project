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
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await context.Products
                .Include(x => x.Category)
                .Include(x => x.Shipping)
                .Include(x => x.Condition)
                .Where(x => x.isDeleted == false)
                .ToListAsync();
        }
        public async Task<Product> GetProduct(int id)
        {
            var product = await context.Products
                .Include(x => x.Category)
                .Include(x => x.Shipping)
                .Include(x => x.Condition)
                .Where(p => p.Id == id)
                .Where(x => x.isDeleted == false)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new Exception("Product doesn't exist");
            }

            return product;
        }

        public async Task<List<Product>> GetFullSearchProducts(int[] categories, string searchParam, int minPrice, int maxPrice, int sortingType, int pageNumber)
        {
            IQueryable<Product> query = context.Products;

            //sorting
            switch(sortingType)
            {
                case 2:
                    query = query.OrderByDescending(x => x.productName);
                    break;
                case 3:
                    query = query.OrderBy(x => x.price);
                    break;
                case 4:
                    query = query.OrderByDescending(x => x.price);
                    break;
                default:
                    query = query.OrderBy(x => x.productName);
                    break;
            }

            //only not deleted items
            query = query.Where(x => x.isDeleted == false);

            if(categories != null)
            {
                query = query.Where(x => categories.Contains(x.Category.Id));
            }

            //price filter
            if (minPrice > 0)
            {
                query = query.Where(x => x.price > minPrice);
            }

            if(maxPrice > 0 && maxPrice > minPrice) {
                query = query.Where(x => x.price < maxPrice);
            }

            //searchParam
            if(searchParam != null && searchParam != "" && searchParam != "undefined")
            {
                query = query.Where(x => x.productName.Contains(searchParam));
            }

            //pagination
            var productPerPage = 12;
            query = query.Skip(productPerPage * pageNumber).Take(productPerPage);

            //joins
            query = query.Include(x => x.Category);
            query = query.Include(x => x.Shipping);
            query = query.Include(x => x.Condition);

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetSearchProducts(string searchParams)
        {
            return await context.Products
                .Include(x => x.Category)
                .Include(x => x.Shipping)
                .Include(x => x.Condition)
                .Where(x => x.productName.Contains(searchParams))
                .Where(x => x.isDeleted == false)
                .ToListAsync();
        }
        public async Task<Product> AddProduct(Product newProduct)
        {
            context.Products.Add(newProduct);
            await context.SaveChangesAsync();

            return await GetProduct(newProduct.Id);
        }
        public async Task<Product> UpdateProduct(Product newProduct, int id)
        {
            var product = await GetProduct(id);

            product.productName = newProduct.productName;
            product.imagePath = newProduct.imagePath;
            product.price = newProduct.price;
            product.description = newProduct.description;
            product.isDeleted = newProduct.isDeleted;
            product.quantity = newProduct.quantity;
            product.categoryId = newProduct.categoryId;
            product.conditionId = newProduct.conditionId;
            product.shippingId = newProduct.shippingId;
            product.isModified = false;
            await context.SaveChangesAsync();


            return await GetProduct(id);
        }
        public async Task<List<Product>> DeleteProduct(Product product)
        {
            product.isDeleted = true;
            await context.SaveChangesAsync();

            return await GetProducts();
        }

        public async Task<Product> DecreaseQuantity(Product product, int quantity)
        {
            product.quantity = product.quantity - quantity;
            await context.SaveChangesAsync();

            return await GetProduct(product.Id);
        }

        public async Task<Product> UpdateIsModified(int id)
        {
            var product = await GetProduct(id);

            product.isModified = !product.isModified;
            await context.SaveChangesAsync();

            return await GetProduct(id);
        }

        public async Task<int> GetProductCount(int[] categories, string searchParam, int minPrice, int maxPrice, int sortingType, int pageNumber)
        {
            IQueryable<Product> query = context.Products;

            //sorting
            switch (sortingType)
            {
                case 2:
                    query = query.OrderByDescending(x => x.productName);
                    break;
                case 3:
                    query = query.OrderBy(x => x.price);
                    break;
                case 4:
                    query = query.OrderByDescending(x => x.price);
                    break;
                default:
                    query = query.OrderBy(x => x.productName);
                    break;
            }

            //only not deleted items
            query = query.Where(x => x.isDeleted == false);

            if (categories != null)
            {
                query = query.Where(x => categories.Contains(x.Category.Id));
            }

            //price filter
            if (minPrice > 0)
            {
                query = query.Where(x => x.price > minPrice);
            }

            if (maxPrice > 0 && maxPrice > minPrice)
            {
                query = query.Where(x => x.price < maxPrice);
            }

            //searchParam
            if (searchParam != null && searchParam != "" && searchParam != "undefined")
            {
                query = query.Where(x => x.productName.Contains(searchParam));
            }

            //joins
            query = query.Include(x => x.Category);
            query = query.Include(x => x.Shipping);
            query = query.Include(x => x.Condition);

            var products = await query.ToListAsync();
            return products.Count;
        }
    }
}
