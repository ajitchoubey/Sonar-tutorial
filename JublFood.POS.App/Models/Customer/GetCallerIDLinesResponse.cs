using System.Collections.Generic;

namespace JublFood.POS.App.Models.Customer
{
    public class GetCallerIDLinesResponse
    {
        public GetCallerIDLinesResult Result { get; set; }
    }
    public class GetCallerIDLinesResult
    {
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public List<GetCallerIDLine> CallerIDLines { get; set; }

    }
    public class GetCallerIDLine
    {
        public string LocationCode { get; set; }
        public string Line { get; set; }
        public string CallDate { get; set; }
        public string CallTime { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public bool Answered { get; set; }
        public int Existing { get; set; }
    }
}
