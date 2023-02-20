namespace JublFood.POS.App.Models.Employee
{
    public class GetEmployeeCodeResponse
    {
        public EmployeeCodeResult Result { get; set; }
    }

    public class EmployeeCodeResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public EmployeeCodeData EmployeeCodeDetail { get; set; }
    }

    public class EmployeeCodeData
    {
        public string EmployeeCode { get; set; }
        public bool RightHanded { get; set; }
        public int LanguageCode { get; set; }
    }
}
