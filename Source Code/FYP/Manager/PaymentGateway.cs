using FYP.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Manager
{
    public class PaymentGateway
    {

        public TransactionModel MakePayment(string name, string number, int month, int year, string cvc, int amount, string email, int CustomerId)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_51LX7xQJ8SwCQZPLgZVTC40ij2hhuZg7CEY2tAcvLkKILKqiMrVPAYF1ZZUpqc9abJ4fjMd8vZnl5Gc1HjTb81U8g00iTTrMxMN");

                TokenCardOptions card = new TokenCardOptions();

                card.Name = name;
                card.Number = number;
                card.ExpYear = year;
                card.ExpMonth = month;
                card.Cvc = cvc;


                TokenCreateOptions tokenCreateOption = new TokenCreateOptions();

                tokenCreateOption.Card = card;

                TokenService tokenService = new TokenService();

                Token token = tokenService.Create(tokenCreateOption);


                CustomerCreateOptions customer = new CustomerCreateOptions();

                customer.Email = email;

                customer.Source = token.Id;

                var custService = new CustomerService();

                Customer stripeCustomer = custService.Create(customer);


                var options = new ChargeCreateOptions
                {
                    Amount = amount * 100,
                    Currency = "PKR",
                    ReceiptEmail = email,
                    Customer = stripeCustomer.Id,
                    Description = email + " has booked trip",
                };


                var service = new ChargeService();

                Charge charge = service.Create(options);

                return new TransactionModel
                {
                    Id = charge.PaymentMethod,
                    Amount = amount.ToString(),
                    Currency = "PKR",
                    Card_Number = "****" + number.Substring(number.Length - 4),
                    Description = email + " has booked trip",
                    Status = charge.Paid,
                    Customer_Id = CustomerId
                };
            }
            catch (Exception)
            {
                return new TransactionModel();
            }
        }
    }
}