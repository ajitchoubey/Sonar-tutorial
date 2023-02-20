using System;

namespace JublFood.POS.App.Models.Customer
{
    public class GetCustomerProfileResponse
    {
        public GetCustomerProfileResult Result { get; set; }
    }

    public class GetCustomerProfileResult
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public GetCustomerProfile CustomerProfile { get; set; }

    }

    public class GetCustomerProfile
    {
        public DateTime FirstOrderDate { get; set; }
        public DateTime LastOrderDate { get; set; }
        public double OrdersPerMonth { get; set; }
        public int Elapsed { get; set; }
        public decimal YTDAverage { get; set; }
        public int YTDOrdersCount { get; set; }
        public decimal YTDOrdersAmount { get; set; }
        public int YTDLateCount { get; set; }
        public decimal YTDLateAmount { get; set; }
        public int YTDBadCount { get; set; }
        public decimal YTDBadAmount { get; set; }
        public int YTDVoidCount { get; set; }
        public decimal YTDVoidAmount { get; set; }
        public decimal LastAverage { get; set; }
        public int LastOrdersCount { get; set; }
        public decimal LastOrdersAmount { get; set; }
        public int LastLateCount { get; set; }
        public decimal LastLateAmount { get; set; }
        public int LastBadCount { get; set; }
        public decimal LastBadAmount { get; set; }
        public int LastVoidCount { get; set; }
        public decimal LastVoidAmount { get; set; }
        public decimal CreditLimit { get; set; }
        public bool AcceptChargeAccount { get; set; }

        public decimal ARBalance { get; set; }

        public decimal InStoreCredit { get; set; }


    }
}
