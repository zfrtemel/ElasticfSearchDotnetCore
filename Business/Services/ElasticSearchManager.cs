using ElasticfSearchDotnetCore.Business.Interfaces;
using ElasticfSearchDotnetCore.Entities.Models;
using Nest;

namespace ElasticfSearchDotnetCore.Business.Services;
public class ElasticSearchManager : IElasticSearchManager
{
    private readonly IElasticClient _elasticClient;

    public ElasticSearchManager(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task IndexProductAsync(Product product)
    {
        var productData = await _elasticClient.IndexDocumentAsync(product);

    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var response = await _elasticClient.GetAsync<Product>(id, idx => idx.Index("products"));
        return response.Source;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
    {
        var response = await _elasticClient.SearchAsync<Product>(s => s
        .Index("products")
        .Query(q => q.Match(m => m.Field(f => f.Name).Query(query))));

        return response.Documents;
    }
    public async Task DeleteProductAsync(int id)
    {
        await _elasticClient.DeleteAsync<Product>(id, d => d.Index("products"));
    }
}
