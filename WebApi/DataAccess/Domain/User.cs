using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCNo { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string PlateNo { get; set; }

        public virtual List<BankCardInfo> BankCards { get; set; }
        public virtual List<Apartment> Apartments { get; set; }
        public virtual List<Message> Messages { get; set; }
        //public virtual List<Payment> Payments { get; set; }

    }
}
