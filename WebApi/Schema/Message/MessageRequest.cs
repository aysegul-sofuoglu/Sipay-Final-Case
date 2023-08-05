namespace Schema
{
    public class MessageRequest
    {
        public int SenderId { get; set; }
        
        public string Mesage { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
