using System;
using Twilio.TwiML;
using Sachtony;
using Twilio.TwiML.Voice;

namespace Sachtony
{  
    class Stripe
    {
       public static global::System.Object StripeAccount { get; private set; }
        public static global::System.Object Console { get; private set; }
        public static global::System.Object apiKey { get; private set; }
        public static global::System.Object MasterKey { get; private set; }
        public static global::System.Object Endpoint { get; private set; }
        static void Main(string[] args)
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "pk_live_1bnGZtQevESu4FHCkDl5REte004QVNFN70";
            const string StripeAccount = "acct_1GLVNREGe1Q3f3kI";
            const string apiKey = "pk_live_1bnGZtQevESu4FHCkDl5REte004QVNFN70";
            const string MasterKey = "rk_live_1Pj75Jo2MU7VAKGZExiE5RfB006dYVlC7t";
            const string TenantId = "f0fae2c0-c182-45fa-87b9-4d2bab321dc5";

            var options = new TopupCreateOptions
            {
                Amount = 2000,
                Currency = "usd",
                Description = "Top-up for Issuing, week of June 28th",
                StatementDescriptor = "Top-up",
            };

            options.AddExtraParam("destination_balance", "issuing");
            var service = new TopupService();
            service.Create(options);

            var BankaccountCountryOptions = {
                Currency = "EUR",
                AccountNumber = "42466924",
                RoutingNumber = "209778",
            };

            var service = new BalanceService();
            Balance balance = service.Get();

            var Endpoint = "https://api.stripe.com/v1/balance";
            var accountBaseUrl = "https://dashboard.stripe.com/b/acct_1GLVNREGe1Q3f3kI";

            var options = new PayoutCreateOptions
            {
                Amount = 1000.00,
                Currency = "usd",
            };
            options.AddExtraParam("source_balance", "issuing");
            var service = new PayoutService();
            var payout = service.Create(options);

            var options = new AccountCreateOptions
            {
                Type = "custom",
                Country = "US",
                RequestedCapabilities = new List<string>
                {
                    "transfers",
                    "card_payments",
                    "card_issuing",
                },
            };
            var service = new AccountService();
            var account = service.Create(options);

            var options = new AccountUpdateOptions
            {
                BusinessType = "company",
                Country = "US",
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Mcc = "6924",
                    Url = "https://www.sachtony.com",
                },
                Company = new AccountCompanyOptions
                {
                    Name = "Sachtony Company Limited",
                    TaxId = "31-4116936",
                    Address = new AddressOptions 
                    {
                        Line1 = "316 Le Van Sy St",
                        City = "Ho chi Minh",
                        State = "",
                        PostalCode = "700000",
                        Country = "VN",
                    },
                    Status = "Verify"
                }
            };

            var service = new AccountService();
            account = service.Update("{{CONNECTED_STRIPE_ACCOUNT_ID}}", options);

            var options = new PersonUpdateOptions
            {
                FirstName = "Linh  ",
                LastName = "Duong",
                Dob = new DobOptions
                {
                    Day = 12,
                    Month = 12,
                    Year = 1983,
                },
                Address = new AddressOptions
                {
                    Line1 = "316 Le Van Sy St",
                    City = "Ho chi Minh",
                    State = "",
                    PostalCode = "700000",
                    Country = "VN",
                },
                SSNLast4 = "9999",
                Email = "admin@sachtony.com",
                Relationship = new PersonRelationshipOptions
                {
                    AccountOpener = true,
                    Owner = true,
                    PercentOwnership = 25.0,
                }
            };
            var service = new PersonService();
            var person = service.Update("{{CONNECTED_STRIPE_ACCOUNT_ID}}", "{{PERSON_ID}}", options);

            var options = new AccountUpdateOptions
            {
                Company = new AccountCompanyOptions
                {
                    OwnersProvided = true,
                }
            };

            var service = new AccountService();
            account = service.Update("{{CONNECTED_STRIPE_ACCOUNT_ID}}", options);
                
            var options = new CardholderCreateOptions
            {
                Billing = new BillingOptions
                {
                    Address = new AddressOptions
                    {
                        Line1 = "316 Le Van Sy St",
                        City = "Ho chi Minh",
                        State = "",
                        PostalCode = "700000",
                        Country = "VN",
                    },
                },
                Email = "jenny.rosen@shop.sachtony.com",
                PhoneNumber = "+18008675309",
                Name = "Jenny Rosen",
                Status = "active",
                Type = "individual",
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = "{{CONNECTED_STRIPE_ACCOUNT_ID}}";

            var service = new CardholderService();
            var cardholder = service.Create(options, requestOptions);

            var options = new TaxIdCreateOptions
            {
                Type = "eu_vat",
                Value = "",
                Status = "active",
            };

            var service = new TaxIdService();
                service.Get
                (
                    "cus_HYgtNNGYSUUTYy",
                    "txi_HYgt9N97lidLQO"
                );

            var response = new VoiceResponse();
                response.Say("Calling Twilio Pay");
                response.Pay(chargeAmount: "20.45",
                action: new Uri("https://twilio.sachtony.com/pay"));

                Console.WriteLine(response.ToString());
            }
        }
    }
}
