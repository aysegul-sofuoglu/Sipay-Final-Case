namespace Schema
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }
        public string Genre { get; set; }
        public int ApartmentId { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        

        public virtual List<PaymentResponse> Payments { get; set; }
    }
}
