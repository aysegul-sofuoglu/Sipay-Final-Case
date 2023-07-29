using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class MessageRequest
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        
        public string Mesage { get; set; }
        public virtual DateTime Date { get; set; }
        //public bool Seen { get; set; }
    }
}
