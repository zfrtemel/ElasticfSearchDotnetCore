using ElasticfSearchDotnetCore.Entities.Models;

namespace ElasticfSearchDotnetCore.Business.Interfaces;

public interface IElasticSearchManager
{
    public Task IndexProductAsync(Product product);
    public Task<Product> GetProductByIdAsync(int id);
    public Task<IEnumerable<Product>> SearchProductsAsync(string query);
    public Task DeleteProductAsync(int id);
}

