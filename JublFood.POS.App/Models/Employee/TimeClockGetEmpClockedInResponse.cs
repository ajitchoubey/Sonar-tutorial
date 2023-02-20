using System;

namespace JublFood.POS.App.Models.Employee
{
    public class TimeClockGetEmpClockedInResponse
    {
        public TimeClockGetEmpClockedInResult Result { get; set; }
    }

    public class TimeClockGetEmpClockedInResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public TimeClockGetEmpClockedIn TimeClockGetEmpClockedIn { get; set; }
    }

    public class TimeClockGetEmpClockedIn
    {
        public string LocationCode { get; set; }
        public string EmployeeCode { get; set; }
        public string PositionCode { get; set; }
        public DateTime SystemDate { get; set; }
        public byte PositionShiftNumber { get; set; }
        public byte DateShiftNumber { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public double BeginOdometer { get; set; }
        public double EndOdometer { get; set; }
        public decimal TillStartingAmount { get; set; }
        public string Position { get; set; }
        public int Driver { get; set; }
        public bool Manager { get; set; }
        public bool Inside { get; set; }
        public bool RequireTill { get; set; }
        public bool RequireFoodLicense { get; set; }
        public bool RequireDL { get; set; }
        public bool RequireCarRegistration { get; set; }
        public bool RequireCarInspection { get; set; }
        public bool RequireInsurance { get; set; }
        public bool RequireMVR { get; set; }
        public int MVRCheckInterval { get; set; }
        public string MVRIntervalCode { get; set; }

    }
}
