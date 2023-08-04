namespace Schema
{
    public class PayResponse
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
