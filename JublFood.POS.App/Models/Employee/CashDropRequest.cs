using System;

namespace JublFood.POS.App.Models.Employee
{
    public class CashDropRequest
    {
        public string Location_Code { get; set; }
        public string Employee_Code { get; set; }
        public DateTime System_Date { get; set; }
        public decimal Amount { get; set; }
        public string Added_By { get; set; }
    }
}
