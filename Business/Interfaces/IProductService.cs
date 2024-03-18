using ElasticfSearchDotnetCore.Entities.Models;

namespace ElasticfSearchDotnetCore.Business.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetSearchedProducts(string query);
        public Task<bool> CreateProduct(Product request);
        public Task<List<Product>> GetAllAsync();
    }
}
