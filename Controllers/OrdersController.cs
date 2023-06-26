using eCommerce.Data;
using eCommerce.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService _productsService;
        public OrdersController(ApplicationDbContext applicationDbContext)
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
