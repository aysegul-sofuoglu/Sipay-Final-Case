using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("Apartment", Schema = "dbo")]
    public class Apartment
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public int BlockId { get; set; }
        public bool Situation { get; set; }
        public int TypeId { get; set; }
        public int Floor { get; set; }
        public int ApartmentNo { get; set; }


        public virtual User User { get; set; }
        public virtual Block Block { get; set; }
        public ApartmentType ApartmentType { get; set; }


        public virtual List<Dues> Dueses { get; set; }
        public virtual List<Invoice> Invoices { get; set; }

    }
}
