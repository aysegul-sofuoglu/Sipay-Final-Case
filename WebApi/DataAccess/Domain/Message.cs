using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("Message", Schema = "dbo")]
    public class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public virtual User User { get; set; }
        public string Mesage { get; set; }
        public virtual DateTime Date { get; set; }
        public bool Seen { get; set; }

      
    }
}
