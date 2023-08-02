﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema
{
    public class PayRequest
    {
        public int FromCardId { get; set; }
        public int ToCardId { get; set; }
        public int DuesId { get; set; }
        public int InvoiceId { get; set; }  
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
