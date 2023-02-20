namespace JublFood.POS.App.Models.Employee
{
    public class ResetPasswordResponse
    {
        public ResetPasswordResult Result { get; set; }
    }

    public class ResetPasswordResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
    }
}
