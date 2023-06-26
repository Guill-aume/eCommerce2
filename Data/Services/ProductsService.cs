using eCommerce.Data.Base;
using eCommerce.Models;

namespace eCommerce.Data.Services
{
    public class ProductsService:EntityBaseRepository<Product>, IProductsService
    {
        private readonly ApplicationDbContext _context;
        public ProductsService(ApplicationDbContext context): base(context) 
        {
            _context = context;   
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails =  _context.Products.FirstOrDefault(x => x.Id == id);
            return  productDetails;
        }
    }
}
