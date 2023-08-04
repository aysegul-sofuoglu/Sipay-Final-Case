namespace Schema
{
    public class MessageResponse
    {
        public int MessageId { get; set; }
        public string Sender { get; set; }
        public string Mesage { get; set; }
        public virtual DateTime Date { get; set; }
        public bool Seen { get; set; }
    }
}
