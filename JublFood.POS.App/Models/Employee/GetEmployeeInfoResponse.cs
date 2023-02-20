using System;

namespace JublFood.POS.App.Models.Employee
{
    public class GetEmployeeInfoResponse
    {
        public GetEmployeeInfoResult Result { get; set; }
    }

    public class GetEmployeeInfoResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public EmployeeInfo EmployeeInfo { get; set; }
    }

    public class EmployeeInfo
    {
        public string LocationCode { get; set; }
        public string EmployeeCode { get; set; }
        public string UserID { get; set; }
        public int StatusCode { get; set; }
        public int AdminLevel { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string GovernmentID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public DateTime DOB { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public string Email { get; set; }
        public string InsuranceCo { get; set; }
        public string InsurancePolicy { get; set; }
        public string InsExpDate { get; set; }
        public string CarRegistration { get; set; }
        public string RegExpiration { get; set; }
        public string LicensePlate { get; set; }
        public string DLNumber { get; set; }
        public string EmergencyPhone { get; set; }
        public int PayTypeCode { get; set; }
        public string FoodHandlerLicense { get; set; }
        public DateTime FoodHandlerExpiration { get; set; }
        public string Track1Data { get; set; }
        public string Track2Data { get; set; }
        public string Track3Data { get; set; }
        public bool AllowKeyboardAuth { get; set; }
        public bool AllowMSRAuth { get; set; }
        public bool AllowBiometricAuth { get; set; }
        public string AddedBy { get; set; }
        public DateTime Added { get; set; }

    }
}
