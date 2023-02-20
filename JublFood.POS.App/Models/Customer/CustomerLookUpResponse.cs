using System;
using System.Collections.Generic;

namespace JublFood.POS.App.Models.Customer
{
    public class CustomerLookUpResponse
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public GetCustomerLookUpResult CustomerDetail { get; set; }
        public List<CustomerAddressResult> customerAddresses { get; set; }
        public List<CustomerPriorityResult> customerPriorities { get; set; }
    }

    public class GetCustomerLookUpResult
    {
        public string LocationCode { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        public int CustomerCode { get; set; }
        public string Name { get; set; }
        //public string CompanyName { get; set; }
        //public string StreetNumber { get; set; }
        //public int StreetCode { get; set; }
        //public int CrossStreetCode { get; set; }
        //public string Suite { get; set; }
        //public string AddressLine2 { get; set; }
        //public string AddressLine3 { get; set; }
        //public string AddressLine4 { get; set; }
        public string MailingAddress { get; set; }
        //public string PostalCode { get; set; }

        public string Street { get; set; }
        public string CrossStreet { get; set; }
        public string CityName { get; set; }
        public string State { get; set; }

        public string Plus4 { get; set; }
        public string Cart { get; set; }
        public string DeliveryPointCode { get; set; }
        public string WalkSequence { get; set; }
        public string AddressType { get; set; }
        public int SetDiscount { get; set; }
        public bool TaxExempt1 { get; set; }
        public string TaxID1 { get; set; }
        public bool TaxExempt2 { get; set; }
        public string TaxID2 { get; set; }
        public bool TaxExempt3 { get; set; }
        public string TaxID3 { get; set; }
        public bool TaxExempt4 { get; set; }
        public string TaxID4 { get; set; }
        public bool AcceptChecks { get; set; }
        public bool AcceptCreditCards { get; set; }
        public bool AcceptGiftCards { get; set; }
        public bool AcceptChargeAccount { get; set; }
        public bool AcceptCash { get; set; }
        public double FinanceChargeRate { get; set; }
        public int PaymentTerms { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime FirstOrderDate { get; set; }
        public DateTime LastOrderDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime Added { get; set; }
        public DateTime dateofbirth { get; set; }
        public DateTime anniversarydate { get; set; }

        public string DriverComments { get; set; }

        public string Comments { get; set; }

        public string ManagerNotes { get; set; }

        public string GSTIN { get; set; }
        public string PANNumber { get; set; }

        //additional fields - bvorderentry
        public decimal Credit { get; set; }
        public string Customer_Street_Name { get; set; }
        public int Customer_City_Code { get; set; }
        public bool HotelorCollege { get; set; }
        public byte DriverCommentsAddUpdateDelete { get; set; }
        public byte NoteAddUpdateDelete { get; set; }
        public float TaxRate1 { get; set; }
        public float TaxRate2 { get; set; }
    }

    public class CustomerPriorityResult
    {
        public string PriorityCustomerSymbol { get; set; }
        public string PriorityCustomerID { get; set; }
    }

    public class CustomerAddressResult
    {
        //public string LocationCode { get; set; }
        public int sequence { get; set; }
        //public int CustomerCode { get; set; }
        public string CompanyName { get; set; }
        public string StreetNumber { get; set; }
        public int StreetCode { get; set; }
        public int CrossStreetCode { get; set; }
        public string Suite { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostalCode { get; set; }

        public bool IsLastAddress { get; set; }

        public string Street_Name { get; set; }
        public string Cross_Street_Name { get; set; }

    }

}
