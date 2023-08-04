namespace Schema
{
    public class PaymentRequest
    {
        public int PaymentId { get; set; }
        public int CardId { get; set; }
        public int DuesId { get; set; }
        public int InvoiceId { get; set; }
        public virtual DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
