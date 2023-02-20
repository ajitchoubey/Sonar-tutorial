using JublFood.POS.App.Models.Cart;


namespace JublFood.POS.App.Models.Payment
{
    public class PaymentRequest
    {
        public string cart_Id { get; set; }
        public CartHeader cartHeader { get; set; }
        public JublFood.POS.App.Models.Customer.Customer Customer { get; set; }
        public int Order_pay_type_code { get; set; }
        public string Cheque_information { get; set; }
        public decimal Order_Amount { get; set; }
        public string Transaction_Number { get; set; }
        public int Payment_Saved_Mode { get; set; }
    }
}
