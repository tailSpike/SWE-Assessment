using NUnit.Framework;

namespace OrderProcessing.Tests
{
    public class PaymentProcessorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SuccessfullPayment()
        {
            string message;
            Order order = new Order()
            {
                Customer = new Customer()
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    PhoneNumber = "(666) 666-6666"
                },
                DescriptionOfGoods = "1000 Batarangs.",
                Payment = new Payment()
                {
                    Amount = 9985.96,
                    paymentType = PaymentTypes.CreditCard,
                    CreditCard = new CreditCard
                    {
                        CardType = CardTypes.AmericanExpress,
                        CardHolderName = "Bruce Wayne",
                        CardNumber = "111111111111111",
                        Cvv = 1234,
                        ExpirationDate = new System.DateTime(2030, 06, 06),
                        BillingAddress = new BillingAddress
                        {
                            BillingStreetAddress1 = "The Wayne Manor",
                            BillingCity = "Gotham",
                            BillingState = "New Jersey",
                            BillingZipCode = "08260"
                        }
                    }
                },
                ShippingAddress = new ShippingAddress()
                {
                    ShippingStreetAddress1 = "The Wayne Manor",
                    ShippingCity = "Gotham",
                    ShippingState = "New Jersey",
                    ShippingZipCode = "08260"
                }
            };

            OrderProcessor orderProcessing = new OrderProcessor(order);
            if (orderProcessing.SubmitOrder(out message))
            {
                Assert.Pass("Payment passed with message : " + message);
            }
            else
            {
                Assert.Fail("Payment failed to process with error : " + message);
            }
            
            
        }
    }
}