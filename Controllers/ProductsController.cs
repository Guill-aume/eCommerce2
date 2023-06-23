using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Products.ToList();
            return View(data);
        }
        public IActionResult Details(int id)
        {
            var data = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            return View(data);
        }
    }
}


