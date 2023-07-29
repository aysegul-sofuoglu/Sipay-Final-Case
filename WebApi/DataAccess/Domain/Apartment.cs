using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("Apartment", Schema = "dbo")]
    public class Apartment
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Block { get; set; }
        public bool Situation { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int ApartmentNo { get; set; }

        public virtual List<Dues> Dueses { get; set; }
        public virtual List<Invoice> Invoices { get; set; }

    }
}
