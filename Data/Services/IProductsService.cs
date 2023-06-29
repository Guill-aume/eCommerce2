using eCommerce.Data.Base;
using eCommerce.Models;

namespace eCommerce.Data.Services
{
    public interface IProductsService: IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task AddNewProductAsync(NewProductVM data);
    }

}
