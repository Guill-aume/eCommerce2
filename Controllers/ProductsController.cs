using eCommerce.Data;
using eCommerce.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        public ProductsController(IProductsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();   
            return View(data);
        }

        //Get: Products/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            return View(productDetail);
        }
    }
}


