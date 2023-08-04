using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("Genre", Schema = "dbo")]
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual List<Invoice> Invoices { get; set; }
    }
}
