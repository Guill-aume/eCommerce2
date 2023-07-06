using eCommerce.Data.Services;
using eCommerce.Models;
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
            if (productDetail != null)
                return View(productDetail);
            else
                return RedirectToAction(nameof(Index));
        }

        //Get: Products/Create
        public IActionResult Create()
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Description = "This is my store descripiton";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}


