using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly MongoClient _mongoClient;
        private readonly IConfiguration _configuration;
        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            _mongoClient = new MongoClient(connectionString);
            var databaseName = _configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var database = _mongoClient.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(_configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);            
        }

        public IMongoCollection<Product> Products { get; }
    }
}
