using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class BankCardInfoResponse
    {
        public int CardId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string CardNo { get; set; }
    }
}
