using JublFood.POS.App.Models.Employee;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JublFood.POS.App.Models.Printing
{
    public class PrintTimeClockConfirmationRequest
    {
        [Required]
        public string LocationCode { get; set; }

        //[Required]
        //public int DeviceID { get; set; }

        [Required]
        public List<DeviceSetting> ObjDeviceSettings { get; set; }

        [Required]
        public string SendText { get; set; }

        [Required]
        public int LineDisplayRow { get; set; }

        [Required]
        public int LineDisplayColumn { get; set; }

        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string Source { get; set; }
    }
}
