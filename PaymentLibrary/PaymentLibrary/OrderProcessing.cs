using System;

namespace OrderProcessing
{

    #region Enumerations
    public enum PaymentTypes
    {
        CreditCard = 1,
        Check = 2,
        DebitCard = 3
    }

    public enum CardTypes
    {
        AmericanExpress = 1,
        Visa = 2,
        MasterCard = 3
    }
    #endregion

    #region Public Classes


    public class OrderProcessor
    {
        private Order _order;

        
        public OrderProcessor(Order order)
        {
            _order = order;
        }

        public bool SubmitOrder(out string message)
        {
            Validate();
            //if (Submit()){
            message = "Payment processed successfully";
            return true;
            //}
            //else
            //{
            //message="stuff happened";
            //return false;
            //}
        }

        public bool SubmitOrder(Order order, out string message)
        {
            _order = order;
            Validate();
            //if (Submit()){
            message = "Payment processed successfully";
            return true;
            //}
            //else
            //{
            //message="stuff happened";
            //return false;
            //}
        }

        private void Validate()
        {
            if (_order == null)
            {
                throw new NullReferenceException("Order object can not be null");
            }
            _order.Validate();

        }
    }

    public class Order
    {
        public Customer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string DescriptionOfGoods { get; set; }
        public Payment Payment { get; set; }

        internal void Validate()
        {
            if (Customer == null)
            {
                throw new ArgumentNullException("Customer");
            }
            else
            {
                Customer.Validate();
            }

            if (ShippingAddress == null)
            {
                throw new ArgumentNullException("ShippingAddress");
            }
            else
            {
                ShippingAddress.Validate();
            }

            if (string.IsNullOrEmpty(DescriptionOfGoods))
            {
                throw new ArgumentException("DescriptionOfGoods can not be null or empty.");
            }

            if (Payment == null)
            {
                throw new ArgumentNullException("Payment");
            }
            else
            {
                Payment.Validate();
            }

        }

    }

    public class Payment
    {
        public PaymentTypes paymentType { get; set; }
        public CreditCard CreditCard { get; set; }
        public DebitCard DebitCard { get; set; }
        public Check Check { get; set; }
        public double Amount { get; set; }

        internal void Validate()
        {
            
            if (paymentType == PaymentTypes.Check)
            {
                Check.Validate();
            }

            if (paymentType == PaymentTypes.CreditCard)
            {
                CreditCard.Validate();
            }

            if (paymentType == PaymentTypes.DebitCard)
            {
                DebitCard.Validate();
            }

            if (Amount <= 0)
            {
                throw new ArgumentException("Amount must be more than zero.");
            }
            
            return;
        }

        public void SetPaymentInformation
            (string cardNumber,
            int cvv,
            DateTime expirationDate,
            PaymentTypes paymentType,
            CardTypes cardType,
            string accountNumber, 
            string routingNumber, 
            string NameOnCheckingAccount,
            string checkNumber,
            double amount)
        {
            if (paymentType == PaymentTypes.CreditCard)
            {
                CreditCard.CardNumber = cardNumber;
                CreditCard.Cvv = cvv;
                CreditCard.ExpirationDate = expirationDate;
                CreditCard.CardType = cardType;
                CreditCard.BillingAddress = new BillingAddress()
                {

                };
            }

            if (paymentType == PaymentTypes.DebitCard)
            {
                DebitCard.CardNumber = cardNumber;
                DebitCard.Cvv = cvv;
                DebitCard.ExpirationDate = expirationDate;
                DebitCard.CardType = cardType;
                DebitCard.BillingAddress = new BillingAddress()
                {

                };
            }

            if (paymentType == PaymentTypes.Check)
            {
                Check.AccountNumber = accountNumber;
                Check.RoutingNumber = routingNumber;
                Check.NameOnAccount = NameOnCheckingAccount;
                Check.CheckNumber = checkNumber;
                Check.BillingAddress = new BillingAddress
                {

                };
            }
            Amount = amount;

        }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                throw new ArgumentException("First name can not be empty.");
            }

            if (string.IsNullOrEmpty(LastName))
            {
                throw new ArgumentException("Last name can not be empty.");
            }

        }
    }

    public class ShippingAddress
    {
        public string ShippingStreetAddress1 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrEmpty(ShippingStreetAddress1))
            {
                throw new ArgumentException("ShippingStreetAddress1 can not be empty.");
            }

            if (string.IsNullOrEmpty(ShippingCity))
            {
                throw new ArgumentException("ShippingCity can not be empty.");
            }

            if (string.IsNullOrEmpty(ShippingState))
            {
                throw new ArgumentException("ShippingState can not be empty.");
            }

            if (string.IsNullOrEmpty(ShippingZipCode))
            {
                throw new ArgumentException("ShippingZipCode can not be empty.");
            }
        }
    }
    public class BillingAddress
    {
        public string BillingStreetAddress1 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrEmpty(BillingStreetAddress1))
            {
                throw new ArgumentException("BillingStreetAddress1 can not be empty.");
            }

            if (string.IsNullOrEmpty(BillingCity))
            {
                throw new ArgumentException("BillingCity can not be empty.");
            }

            if (string.IsNullOrEmpty(BillingState))
            {
                throw new ArgumentException("BillingState can not be empty.");
            }

            if (string.IsNullOrEmpty(BillingZipCode))
            {
                throw new ArgumentException("BillingZipCode can not be empty.");
            }
        }
    }

    public class CreditCard
    {
        public CardTypes CardType { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvv { get; set; }
        public BillingAddress BillingAddress { get; set; }

        internal void Validate()
        {
            if (((CardType == CardTypes.MasterCard || CardType == CardTypes.Visa) && CardNumber.Length != 16) ||
                ((CardType == CardTypes.AmericanExpress) && CardNumber.Length != 15))
            {
                throw new Exception("Credit card number length doesn't match type requirement.");
            }

            if (((CardType == CardTypes.MasterCard || CardType == CardTypes.Visa) && Cvv.ToString().Length != 3) ||
                ((CardType == CardTypes.AmericanExpress) && Cvv.ToString().Length != 4))
            {
                throw new Exception("cvv exceeds maximum length");
            }
            if (ExpirationDate == default(DateTime))
            {
                throw new Exception("no expiration date was selected");
            }

            if (ExpirationDate < DateTime.Now)
            {
                throw new Exception("Expiration date is invalid.");
            }

            if (BillingAddress == null)
            {
                throw new ArgumentNullException("BillingAddress");
            }

            BillingAddress.Validate();
        }
    }

    public class Check
    {
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string NameOnAccount { get; set; }
        public string CheckNumber { get; set; }
        public BillingAddress BillingAddress { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(AccountNumber))
            {
                throw new Exception("Missing account number.");
            }

            if (string.IsNullOrEmpty(RoutingNumber))
            {
                throw new Exception("Missing account routing number.");
            }

            if (string.IsNullOrEmpty(NameOnAccount))
            {
                throw new Exception("Missing Name of account holder.");
            }

            if (BillingAddress == null)
            {
                throw new ArgumentNullException("BillingAddress");
            }

            BillingAddress.Validate();
        }

    }
    public class DebitCard
    {
        public CardTypes CardType { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvv { get; set; }
        public BillingAddress BillingAddress { get; set; }

        public void Validate()
        {
            if (((CardType == CardTypes.MasterCard || CardType == CardTypes.Visa) && CardNumber.Length != 16) ||
                ((CardType == CardTypes.AmericanExpress) && CardNumber.Length != 15))
            {
                throw new Exception("Credit card number length doesn't match type requirement.");
            }

            if (((CardType == CardTypes.MasterCard || CardType == CardTypes.Visa) && Cvv.ToString().Length != 3) ||
                ((CardType == CardTypes.AmericanExpress) && Cvv.ToString().Length != 4))
            {
                throw new ArgumentException("cvv doesn't match required length.");
            }
            if (ExpirationDate == default)
            {
                throw new ArgumentException("no expiration date was selected");
            }

            if (ExpirationDate < DateTime.Now)
            {
                throw new ArgumentException("Expiration date is invalid.");
            }

            if (BillingAddress == null)
            {
                throw new ArgumentNullException("BillingAddress");
            }

            BillingAddress.Validate();
        }
    }

    #endregion

}
