using BraintreePaymentCore.Web.Utility.PaymentGateway;
using Microsoft.AspNetCore.Mvc;
using Braintree;
using eCommerce.Models;

namespace eCommerce.Controllers
{
    public class PaymentsController : Controller
    {
        public IBraintreeConfiguration _brainTreeConfig = new BraintreeConfiguration();

        public PaymentsController(IBraintreeConfiguration braintreeConfiguration)
        {
            _brainTreeConfig = braintreeConfiguration;
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
        public object Checkout(vmCheckout model)
        {
            string paymentStatus = string.Empty;
            var gateway = _brainTreeConfig.GetGateway();

            var request = new TransactionRequest
            {
                Amount = model.Price,
                PaymentMethodNonce = model.PaymentMethodNonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                paymentStatus = "Succeded";

                //Do Database Operations Here
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

        public IActionResult Index()
        {
            var gateway = _brainTreeConfig.GetGateway();
            var clientToken = gateway.ClientToken.Generate();  //Genarate a token
            ViewBag.ClientToken = clientToken;

            var data = new vmCheckout
            {
                Id = 2,
                
                Price = 230,
                PaymentMethodNonce = ""
            };

            return View(data); //
        }
    }
    
}
