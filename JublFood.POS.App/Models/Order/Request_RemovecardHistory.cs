using System;

namespace JublFood.POS.App.Models.Order
{
    public class Request_RemovecardHistory
    {
        public string Removecart_Id { get; set; }
        public int Order_Pay_type { get; set; }
        public DateTime Order_Date { get; set; }
        public string Transaction_number { get; set; }
        public decimal Amount { get; set; }
    }
}
