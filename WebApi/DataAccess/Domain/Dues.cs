using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("Dues", Schema = "dbo")]
    public class Dues
    {
        public int DuesId { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }

        public virtual List<Payment> Payments { get; set; } 
    }
}
