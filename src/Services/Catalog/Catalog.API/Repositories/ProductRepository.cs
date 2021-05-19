﻿using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext
                .Products
                .InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>
                .Filter.Eq(p => p.Id, id);

            var result = await _catalogContext
                 .Products
                 .DeleteOneAsync(filter);

            return result.IsAcknowledged 
                && result.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>
                .Filter
                .Eq(p => p.Id, id);

            return await _catalogContext
                .Products
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryName(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>
                .Filter.Eq(p => p.Category, categoryName);

            return await _catalogContext
                .Products
                .Find(filter)
                .ToListAsync();            
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>
                .Filter.Eq(p => p.Name, name);

            return await _catalogContext
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext
                .Products
                .Find(p => true)
                .ToListAsync();                
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _catalogContext
                .Products
                .ReplaceOneAsync(p => p.Id.Equals(product.Id), product);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
