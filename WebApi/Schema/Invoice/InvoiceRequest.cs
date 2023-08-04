namespace Schema
{
    public class InvoiceRequest
    {
        public int GenreId { get; set; }
        public int ApartmentId { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
    }
}
