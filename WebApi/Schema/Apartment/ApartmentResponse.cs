using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class ApartmentResponse
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public string Block { get; set; }
        public bool Situation { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int ApartmentNo { get; set; }

        public virtual List<DuesResponse> Dueses { get; set; }
        public virtual List<InvoiceResponse> Invoices { get; set; }
    }
}
