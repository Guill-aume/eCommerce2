using BraintreePaymentCore.Web.Utility.PaymentGateway;
using Microsoft.AspNetCore.Mvc;
using Braintree;
using eCommerce.Data.Cart;
using eCommerce.Data.ViewModels;
using eCommerce.Models;

namespace eCommerce.Controllers
{
    public class PaymentsController : Controller
    {
        public IBraintreeConfiguration _brainTreeConfig = new BraintreeConfiguration();
        private readonly ShoppingCart _shoppingCart;
        public PaymentsController(IBraintreeConfiguration braintreeConfiguration, ShoppingCart shoppingCart)
        {
            _brainTreeConfig = braintreeConfiguration;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Payment()
        {
            var gateway = _brainTreeConfig.GetGateway();
            var clientToken = gateway.ClientToken.Generate();  //Genarate a token
            ViewBag.ClientToken = clientToken;

            var data = new CheckoutVM
            {
                ShoppingCartId = _shoppingCart.ShoppingCartId,
                TotalPrice = _shoppingCart.GetShoppingCartTotal(),
                PaymentMethodNonce = ""
            };

            return View(data); //
        }

        public static readonly TransactionStatus[] transactionSuccessStatuses =
            {
                TransactionStatus.AUTHORIZED,
                TransactionStatus.AUTHORIZING,
                TransactionStatus.SETTLED,
                TransactionStatus.SETTLING,
                TransactionStatus.SETTLEMENT_CONFIRMED,
                TransactionStatus.SETTLEMENT_PENDING,
                TransactionStatus.SUBMITTED_FOR_SETTLEMENT
            };

        [HttpGet, Route("GenerateToken")]
        public object GenerateToken()
        {
            var gateway = _brainTreeConfig.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            return clientToken;
        }

        [HttpPost, Route("Checkout")]
        public object Checkout(CheckoutVM model)
        {
            string paymentStatus = string.Empty;
            var gateway = _brainTreeConfig.GetGateway();

            var request = new TransactionRequest
            {
                Amount = (int)model.TotalPrice,
                PaymentMethodNonce = model.PaymentMethodNonce,
                OrderId = model.ShoppingCartId,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                paymentStatus = "Succeded" + " | TransactionStatus: " + result.Transaction.Status.ToString();

                //Do Database Operations Here
                string ShoppingCartId = result.Transaction.OrderId;
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }

                paymentStatus = errorMessages;
            }

            return paymentStatus;
        }

    }
    
}
