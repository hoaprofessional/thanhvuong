using System;

namespace Framework.InputModels.QoutationManagement.QoutationDetail
{
    public class RecievedMoneyInput
    {
        public int QoutationId { get; set; }
        public String ReceiveMoneyType { get; set; }
    }
    public class QuotesPriceInput
    {
        public int QoutationId { get; set; }
        public String QuotesType { get; set; }
    }
}
