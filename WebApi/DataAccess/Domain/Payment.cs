using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("Payment", Schema = "dbo")]
    public class Payment
    {
        public int PaymentId { get; set; }
        public int CardId { get; set; }
        public virtual BankCardInfo BankCardInfo { get; set; }
        //public int UserId { get; set; }
        //public virtual User User { get; set; }
        public int DuesId { get; set; }
        public virtual Dues Dues { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual DateTime Date { get; set; }
        public decimal Amount { get; set; }

      


    }
}
