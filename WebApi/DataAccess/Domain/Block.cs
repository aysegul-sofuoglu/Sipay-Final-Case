using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("Block", Schema = "dbo")]
    public class Block
    {
        public int BlockId { get; set; }
        public string Name { get; set; }


        public virtual List<Apartment> Apartments { get; set; }
    }
}
