namespace Schema
{
    public class BankCardInfoResponse
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Balance { get; set; }
        public string CardNo { get; set; }
    }
}
