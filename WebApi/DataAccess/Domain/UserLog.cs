using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("UserLog", Schema = "dbo")]
    public class UserLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string LogType { get; set; }
    }
}
