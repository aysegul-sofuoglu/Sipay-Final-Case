namespace Schema
{
    public class DuesResponse
    {
        public int DuesId { get; set; }
        public int ApartmentId { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        

        public virtual List<PaymentResponse> Payments { get; set; }
    }
}
