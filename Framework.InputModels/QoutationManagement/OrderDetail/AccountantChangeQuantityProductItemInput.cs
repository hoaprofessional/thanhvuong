namespace Framework.InputModels.QoutationManagement.OrderDetail
{
    public class AccountantChangeQuantityProductItemInput
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
