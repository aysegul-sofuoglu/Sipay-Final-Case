using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Domain
{
    [Table("Invoice", Schema = "dbo")]
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int GenreId { get; set; }
        public int ApartmentId { get; set; }
        public int Mounth { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }



        public virtual Genre Genre { get; set; }
        public virtual Apartment Apartment { get; set; }

        public virtual List<Payment> Payments { get; set; }


    }
}
