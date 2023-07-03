using Braintree;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BraintreePaymentCore.Web.Utility.PaymentGateway
{
    public class BraintreeConfiguration : IBraintreeConfiguration
    {
        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        private IBraintreeGateway BraintreeGateway { get; set; }

        public IBraintreeGateway CreateGateway()
        {
            Environment = System.Configuration.ConfigurationManager.AppSettings["BraintreeEnvironment"];
            MerchantId = System.Configuration.ConfigurationManager.AppSettings["BraintreeMerchantId"];
            PublicKey = System.Configuration.ConfigurationManager.AppSettings["BraintreePublicKey"];
            PrivateKey = System.Configuration.ConfigurationManager.AppSettings["BraintreePrivateKey"];

            if (MerchantId == null || PublicKey == null || PrivateKey == null)
            {
                Environment = "SANDBOX";
                MerchantId = "gvxpthbmygvvsprx";
                PublicKey = "38n7ggb9vcc36p3r";
                PrivateKey = "8ee6e4c780607b7979fc97389c3289ae";
            }

            return new BraintreeGateway(Environment, MerchantId, PublicKey, PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (BraintreeGateway == null)
            {
                BraintreeGateway = CreateGateway();
            }

            return BraintreeGateway;
        }
    }
}