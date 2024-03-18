using ElasticfSearchDotnetCore.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElasticfSearchDotnetCore.Controllers;

[Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IElasticSearchManager _elasticSearchManager;
    public ProductController(IProductService productService, IElasticSearchManager elasticSearchManager)
    {
        _productService = productService;
        _elasticSearchManager = elasticSearchManager;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string q)
    {
        //var products = await _productService.GetAllAsync();
        //products.ForEach(async p => await _elasticSearchManager.IndexProductAsync(p));
        //return Ok("ok");
        return Ok(await _elasticSearchManager.SearchProductsAsync(q));
    }
}
