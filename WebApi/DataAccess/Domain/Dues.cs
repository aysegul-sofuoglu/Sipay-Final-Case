using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public decimal Amounth { get; set; }

        public virtual List<Payment> Payments { get; set; } 
    }
}
