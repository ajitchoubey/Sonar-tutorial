namespace JublFood.POS.App.Models
{
    public class CheckBiometricDataResponse
    {
        public CheckBiometricDataResult Result { get; set; }
    }

    public class CheckBiometricDataResult
    {
        public string ResponseStatusCode { get; set; }
        public string ResponseStatus { get; set; }
        public CheckBiometricData BiometricData { get; set; }
    }

    public class CheckBiometricData
    {
        public string BiometricPunch { get; set; }
        public bool OpenClockInUser { get; set; }

    }
}
