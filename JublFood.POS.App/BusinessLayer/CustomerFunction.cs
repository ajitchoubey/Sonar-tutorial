using JublFood.POS.App.API;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Customer;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JublFood.POS.App.BusinessLayer
{
    public class CustomerFunction
    {        
        
        public static Customer GetCustomer(string Location_Code, string Phone_Number, string Extn_Number)
        {
            Result CustomerResult;
            List<CustomerAddressResult> customerAddresses = new List<CustomerAddressResult>();
            CustomerAddressResult customerAddressResult = new CustomerAddressResult();
            Customer customer = new Customer();
            CustomerLookUpRequest requestModel = new CustomerLookUpRequest();

            requestModel.LocationCode = Location_Code;
            requestModel.PhoneNumber = Phone_Number;
            requestModel.PhoneNumberExt = "";

            CustomerLookUpResponse apiResponse = APILayer.CustomerLookUp(APILayer.CallType.POST, requestModel);
            if (apiResponse != null && apiResponse.Result != null)
            {

                CustomerResult = apiResponse.Result;
                customerAddresses = apiResponse.Result.customerAddresses;

                if (apiResponse.Result.CustomerDetail != null)
                {
                    if (apiResponse.Result.customerAddresses != null && apiResponse.Result.customerAddresses.Count > 0)
                    {
                        foreach (CustomerAddressResult address in apiResponse.Result.customerAddresses)
                        {
                            if (address.IsLastAddress)
                            {
                                customerAddressResult.CompanyName = address.CompanyName;
                                customerAddressResult.StreetNumber = address.StreetNumber;
                                Session.blnmodify = address.Street_Name;
                                customerAddressResult.Street_Name = address.Street_Name;
                                customerAddressResult.AddressLine2 = address.AddressLine2;
                                customerAddressResult.AddressLine3 = address.AddressLine3;
                                customerAddressResult.AddressLine4 = address.AddressLine4;
                                customerAddressResult.PostalCode = address.PostalCode;
                                customerAddressResult.Suite = address.Suite;
                                customerAddressResult.StreetCode = address.StreetCode;
                                break;
                            }
                            else
                            {
                                Session.blnmodify = address.Street_Name;
                                customerAddressResult.PostalCode = address.PostalCode;
                                customerAddressResult.StreetCode = address.StreetCode;
                            }
                        }
                    }

                    customer.Location_Code = CustomerResult.CustomerDetail.LocationCode;
                    if (CustomerResult.CustomerDetail.PhoneNumber == null)
                        customer.Phone_Number = "";
                    else

                    customer.Phone_Number = CustomerResult.CustomerDetail.PhoneNumber;
                    if (CustomerResult.CustomerDetail.PhoneExt == null)
                        customer.Phone_Ext = "";
                    else
                    customer.Phone_Ext = CustomerResult.CustomerDetail.PhoneExt;
                    if (CustomerResult.CustomerDetail.CustomerCode == null)
                        CustomerResult.CustomerDetail.CustomerCode = 0;
                    else
                        customer.Customer_Code = CustomerResult.CustomerDetail.CustomerCode;

                    customer.Name = CustomerResult.CustomerDetail.Name;
                    customer.Company_Name = customerAddressResult.CompanyName;
                    customer.Street_Number =customerAddressResult.StreetNumber;
                    customer.Street_Code = customerAddressResult.StreetCode;
                    customer.Cross_Street_Code = customerAddressResult.CrossStreetCode;
                    customer.Suite = customerAddressResult.Suite;
                    customer.Address_Line_2 = customerAddressResult.AddressLine2;
                    customer.Address_Line_3 = customerAddressResult.AddressLine3;
                    customer.Address_Line_4 = customerAddressResult.AddressLine4;
                    if (CustomerResult.CustomerDetail.MailingAddress == null)
                        customer.Mailing_Address = "";
                    else
                    customer.Mailing_Address = CustomerResult.CustomerDetail.MailingAddress;

                    customer.Postal_Code = customerAddressResult.PostalCode;
                    if (CustomerResult.CustomerDetail.Plus4 == null)
                        customer.Plus4 = "";
                    else

                        customer.Plus4 = CustomerResult.CustomerDetail.Plus4;

                    customer.Cart = CustomerResult.CustomerDetail.Cart;
                    if (CustomerResult.CustomerDetail.DeliveryPointCode == null)
                        customer.Delivery_Point_Code = "";
                    else
                    customer.Delivery_Point_Code = CustomerResult.CustomerDetail.DeliveryPointCode;

                    if (CustomerResult.CustomerDetail.WalkSequence == null)
                        customer.Walk_Sequence = "";
                    else

                    customer.Walk_Sequence = CustomerResult.CustomerDetail.WalkSequence;

                    customer.Address_Type = CustomerResult.CustomerDetail.AddressType;

                    customer.Set_Discount = CustomerResult.CustomerDetail.SetDiscount;
                    customer.Tax_Exempt1 = CustomerResult.CustomerDetail.TaxExempt1;
                    if (CustomerResult.CustomerDetail.TaxID1 == null)
                        customer.Tax_ID1 = "";
                    else
                    customer.Tax_ID1 = CustomerResult.CustomerDetail.TaxID1;

                    customer.Tax_Exempt2 = CustomerResult.CustomerDetail.TaxExempt2;
                    if (CustomerResult.CustomerDetail.TaxID2 == null)
                        customer.Tax_ID2 = "";
                    else
                    customer.Tax_ID2 = CustomerResult.CustomerDetail.TaxID2;

                    customer.Tax_Exempt3 = CustomerResult.CustomerDetail.TaxExempt3;

                    if (CustomerResult.CustomerDetail.TaxID3 == null)
                        customer.Tax_ID3 = "";
                    else
                    customer.Tax_ID3 = CustomerResult.CustomerDetail.TaxID3;

                    customer.Tax_Exempt4 = CustomerResult.CustomerDetail.TaxExempt4;
                    if (CustomerResult.CustomerDetail.TaxID4 == null)
                        customer.Tax_ID4 = "";
                    else
                    customer.Tax_ID4 = CustomerResult.CustomerDetail.TaxID4;

                    customer.Accept_Checks = CustomerResult.CustomerDetail.AcceptChecks;
                    customer.Accept_Credit_Cards = CustomerResult.CustomerDetail.AcceptCreditCards;
                    customer.Accept_Gift_Cards = CustomerResult.CustomerDetail.AcceptGiftCards;
                    customer.Accept_Charge_Account = CustomerResult.CustomerDetail.AcceptChargeAccount;
                    customer.Accept_Cash = CustomerResult.CustomerDetail.AcceptCash;
                    customer.Finance_Charge_Rate = CustomerResult.CustomerDetail.FinanceChargeRate;
                    customer.Credit_Limit = CustomerResult.CustomerDetail.CreditLimit;
                    customer.Credit = CustomerResult.CustomerDetail.CreditLimit;
                    customer.Payment_Terms = CustomerResult.CustomerDetail.PaymentTerms;
                    customer.First_Order_Date = CustomerResult.CustomerDetail.FirstOrderDate;
                    customer.Last_Order_Date = CustomerResult.CustomerDetail.LastOrderDate;
                    customer.Added_By = CustomerResult.CustomerDetail.AddedBy;
                    customer.Comments = CustomerResult.CustomerDetail.Comments;
                    if (CustomerResult.CustomerDetail.DriverComments == null)
                        customer.DriverComments = "";
                    else
                    customer.DriverComments = CustomerResult.CustomerDetail.DriverComments;

                    customer.DriverCommentsAddUpdateDelete = false;
                    if (CustomerResult.CustomerDetail.ManagerNotes == null)
                        customer.Manager_Notes = "";
                    else
                    customer.Manager_Notes = CustomerResult.CustomerDetail.ManagerNotes;

                    customer.Customer_City_Code = CustomerResult.CustomerDetail.Customer_City_Code;
                    customer.Customer_Street_Name = CustomerResult.CustomerDetail.Street;
                    customer.HotelorCollege = false;
                    customer.City = CustomerResult.CustomerDetail.CityName;
                    customer.Region = CustomerResult.CustomerDetail.State;
                    customer.TaxRate1 = 0;
                    customer.TaxRate2 = 0;
                    customer.Cross_Street = CustomerResult.CustomerDetail.CrossStreet;
                    customer.NoteAddUpdateDelete = false;
                    customer.gstin_number = CustomerResult.CustomerDetail.GSTIN;
                    customer.date_of_birth = CustomerResult.CustomerDetail.dateofbirth;
                    customer.anniversary_date = CustomerResult.CustomerDetail.anniversarydate;
                    //customer.Action = Constants.Source;
                }
                
            }
            return customer;
        }
    }
}
