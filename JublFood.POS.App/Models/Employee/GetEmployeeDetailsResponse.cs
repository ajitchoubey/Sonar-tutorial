namespace JublFood.POS.App.Models.Employee
{
    public class GetEmployeeDetailsResponse
    {
        public EmployeeDetailResult Result { get; set; }
    }

    public class EmployeeDetailResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
    }

    public class EmployeeDetail
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int ResetPassword { get; set; }
    }
}
