using System.Collections.Generic;

namespace JublFood.POS.App.Models.Employee
{
    public class GetEmployeePositionResponse
    {
        public EmployeePositionResult Result { get; set; }
    }

    public class EmployeePositionResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public List<EmployeePosition> employeePositions { get; set; }
    }

    public class EmployeePosition
    {
        public string LocationCode { get; set; }
        public string EmployeeCode { get; set; }
        public string PositionCode { get; set; }
        public decimal PayRate { get; set; }
        public string Position { get; set; }
        public bool Driver { get; set; }
        public bool Manager { get; set; }
        public bool Inside { get; set; }
        public bool RequireTill { get; set; }
        public decimal TillStartingAmount { get; set; }
        public bool RequireFoodLicense { get; set; }
        public bool RequireDL { get; set; }
        public bool RequireCarRegistration { get; set; }
        public bool RequireCarInspection { get; set; }
        public bool RequireInsurance { get; set; }
        public bool RequireMVR { get; set; }
        public int MVRCheckInterval { get; set; }
        public string MVRIntervalCode { get; set; }
        public string PositionImage { get; set; }
        public string TextColor { get; set; }
        public string Addedby { get; set; }

    }
}
