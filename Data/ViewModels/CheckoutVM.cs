using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Data.Cart;
using eCommerce.Models;

namespace eCommerce.Data.ViewModels
{
    public class CheckoutVM : ShoppingCartVM
    {
        public string ShoppingCartId { get; set; }
        public double TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PaymentMethodNonce { get; set; }
    }
}
