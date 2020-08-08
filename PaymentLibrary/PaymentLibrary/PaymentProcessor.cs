using System;

namespace PaymentLibrary
{
    public class CreditCard
    {}

    public class Check
    { }
    public class DebitCard
    { }

    public enum PaymentTypes
    {
        CreditCard=1,
        Check=2,
        DebitCard=3
    }

    public class PaymentProcessor {
        public string creditCardNumber { get; set; }
        public DateTime exporationDate { get; set; }
        public string cvv { get; set; }

        private void ValidateCheck(string accountNumber, string routingNumber)
        {
            return;
        }

        private void Validate(string creditCardNumber, string cvv, DateTime? expirationDate)
        {
            if (creditCardNumber.Length != 16)
            {
                throw new Exception("Credit card number exceeds maximum length");
            }

            if (cvv.Length != 3)
            {
                throw new Exception("cvv exceeds maximum length");
            }
            if (expirationDate == null)
            {
                throw new Exception("no expiration date was selected");
            }


            return;
        }

        public bool ProcessPayment(string accountNumber, string routingNumber, double amount)
        {
            ValidateCheck(accountNumber, routingNumber);
            return true;

        }

        public bool ProcessPayment(string creditCardNumber, string cvv, DateTime? expirationDate, PaymentTypes paymentType, double amount)
        {
            if (paymentType == PaymentTypes.CreditCard || paymentType == PaymentTypes.DebitCard)
            {
                Validate(creditCardNumber, cvv, expirationDate);

            }

            //Use library to send processing information to end point, and manage response.

            return true;

        }
    }


}
