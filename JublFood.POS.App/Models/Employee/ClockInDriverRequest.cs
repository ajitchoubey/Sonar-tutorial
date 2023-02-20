using System;

namespace JublFood.POS.App.Models.Employee
{
    public class ClockInDriverRequest
    {
        public string Location_code { get; set; }
        public DateTime System_date { get; set; }
    }
   
}
