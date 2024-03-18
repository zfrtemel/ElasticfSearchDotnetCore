using Microsoft.AspNetCore.Mvc;

namespace ElasticfSearchDotnetCore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
