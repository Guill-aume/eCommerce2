using eCommerce.Data;
using eCommerce.Data.Cart;
using eCommerce.Data.Services;
using eCommerce.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IProductsService productsService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productsService = productsService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
           // string userId = User.Identity.Name;
           // string a = User.GetUserId();
            string email = User.Identity.Name;//this.User.FindFirst(ClaimTypes.NameIdentifier).ToString();

          //  userId = userId.Substring(70);
            var orders = await _ordersService.GetOrdersByEmailAsync(email);
            return View(orders); 
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()

            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);
            if (item != null) {
            _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder(int id)
        {
            var items =  _shoppingCart.GetShoppingCartItems();           
            string userId = "";
            string userEmailAddress = User.Identity.Name; 
            await _ordersService.StoreOrderAsync(items,userId,userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
