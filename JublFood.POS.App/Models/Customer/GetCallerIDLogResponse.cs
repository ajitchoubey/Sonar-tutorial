using System.Collections.Generic;

namespace JublFood.POS.App.Models.Customer
{
    public class GetCallerIDLogResponse
    {
        public GetCallerIDLogResult Result { get; set; }
    }
    public class GetCallerIDLogResult
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<GetCallerIDLog> CallerIDLogs { get; set; }

    }

    public class GetCallerIDLog
    {
        public string CallDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }
    }
}
