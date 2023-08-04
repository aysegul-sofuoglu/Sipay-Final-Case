namespace Schema
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public string CardId { get; set; }
        public string UserName { get; set; }
        public int DuesId { get; set; }
        public int InvoiceId { get; set; }
        public virtual DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
