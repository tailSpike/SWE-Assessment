using System;

namespace PaymentLibrary
{
    public class Order
    {
        public Customer Customer { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string DescriptionOfGoods { get; set; }

        //One of these payment objects must be none-null
        public CreditCard CreditCard { get; set; }
        public Check Check { get; set; }
        public DebitCard DebitCard { get; set; }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
    }

    public class ShippingAddress
    {
        public string ShippingStreetAddress1 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
    }
    public class BillingAddress
    {
        public string BillingStreetAddress1 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }
    }

    public class CreditCard
    {
        public CardTypes? CardType { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvv { get; set; }
        public BillingAddress Address { get; set; }
    }

    public class Check
    {
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public string NameOnAccount { get; set; }
        public string CheckNumber { get; set; }
        public BillingAddress Address { get; set; }

    }
    public class DebitCard
    {
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvv { get; set; }
        public BillingAddress Address { get; set; }

    }

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

    class OrderProcessor
    {
        private Order _order;
        Payment payment = new Payment();

        public OrderProcessor(Order order)
        {
            _order = order;
        }

        public void SubmitOrder()
        {

        }

        public void SubmitOrder(Order order)
        {

        }
    }
    class Payment
    {
        public PaymentTypes paymentType { get; set; }
        public CreditCard CreditCard { get; set; }
        public DebitCard DebitCard { get; set; }
        public Check Check { get; set; }
        public double Amount { get; set; }

        private void ValidateCheck(string accountNumber, string routingNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                throw new Exception("Missing account number.");
            }

            if (string.IsNullOrEmpty(routingNumber))
            {
                throw new Exception("Missing account routing number.");
            }
            return;
        }

        private void Validate(string creditCardNumber, int cvv, DateTime expirationDate, CardTypes cardType)
        {

            if (((cardType == CardTypes.MasterCard || cardType == CardTypes.Visa) && creditCardNumber.Length != 16) ||
                ((cardType == CardTypes.AmericanExpress) && creditCardNumber.Length != 15))
            {
                throw new Exception("Credit card number length doesn't match type requirement.");
            }

            if (((cardType == CardTypes.MasterCard || cardType == CardTypes.Visa) && cvv.Length != 3) ||
                ((cardType == CardTypes.AmericanExpress) && cvv.Length != 4))
            {
                throw new Exception("cvv exceeds maximum length");
            }
            if (expirationDate == default(DateTime))
            {
                throw new Exception("no expiration date was selected");
            }

            if (expirationDate < DateTime.Now)
            {
                throw new Exception("Expiration date is invalid.");
            }


            return;
        }

        private bool SetCheckPayment
            (string accountNumber, string routingNumber, double amount)
        {
            ValidateCheck(accountNumber, routingNumber);
            return true;

        }

        private bool SetCardPayment
            (string creditCardNumber,
            int cvv,
            DateTime expirationDate,
            PaymentTypes paymentType,
            CardTypes cardType,
            double amount)
        {
            if (paymentType == PaymentTypes.CreditCard || paymentType == PaymentTypes.DebitCard)
            {
                Validate(creditCardNumber, cvv, expirationDate, cardType);

            }
            CreditCard.CardNumber = creditCardNumber;
            CreditCard.Cvv = cvv;
            CreditCard.ExpirationDate = expirationDate;
            CreditCard.CardType = cardType;
            CreditCard.Address = new BillingAddress()
            {

            };

            return true;
        }
    }


}
