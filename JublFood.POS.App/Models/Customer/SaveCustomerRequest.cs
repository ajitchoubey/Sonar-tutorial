using System;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Customer
{
    public class SaveCustomerRequest
    {
        [Required]
        public string LocationCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string PhoneExt { get; set; }
        [Required]
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string StreetName { get; set; }
        public string CrossStreetName { get; set; }
        public string StreetNumber { get; set; }
        public int StreetCode { get; set; }
        public int CrossStreetCode { get; set; }
        public string Suite { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string PostalCode { get; set; }
        public string AddressType { get; set; }
        public bool TaxExempt1 { get; set; }
        public string TaxID1 { get; set; }
        public bool TaxExempt2 { get; set; }
        public string TaxID2 { get; set; }
        public bool TaxExempt3 { get; set; }
        public string TaxID3 { get; set; }
        public bool TaxExempt4 { get; set; }
        public string TaxID4 { get; set; }
        public double FinanceChargeRate { get; set; }
        public DateTime FirstOrderDate { get; set; }
        public DateTime LastOrderDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime dateofbirth { get; set; }
        public DateTime anniversarydate { get; set; }
        public string DriverComments { get; set; }
        public string Comments { get; set; }
        public string ManagerNotes { get; set; }
        public string GSTIN { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        //manish
        public bool Accept_Charge_Account { get; set; }
        public bool Accept_Credit_Cards { get; set; }

    }
}
