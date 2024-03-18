using ElasticfSearchDotnetCore.Business.Interfaces;
using ElasticfSearchDotnetCore.Entities.Context;
using ElasticfSearchDotnetCore.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ElasticfSearchDotnetCore.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _context;
        private readonly IElasticSearchManager _elasticSearchManager;

        public ProductService(MyDbContext context, IElasticSearchManager elasticSearchManager)
        {
            _context = context;
            _elasticSearchManager = elasticSearchManager;
        }

        public async Task<bool> CreateProduct(Product request)
        {
            _context.Products.Add(request);
            await _context.SaveChangesAsync();
            await _elasticSearchManager.IndexProductAsync(request);
            return true;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetSearchedProducts(string query)
        {
            var sqlQuery = $"SELECT * FROM Products WHERE CONTAINS(Name, '\"{query}*\"')";
            var products = await _context.Products.FromSqlRaw(sqlQuery).ToListAsync();
            return products;
        }
    }
}
