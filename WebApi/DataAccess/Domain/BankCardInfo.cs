using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("BankCardInfo", Schema = "dbo")]
    public class BankCardInfo
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public decimal Balance { get; set; }
        public string CardNo { get; set; }

        
    }
}
