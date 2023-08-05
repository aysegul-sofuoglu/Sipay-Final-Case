using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Domain
{
    [Table("ApartmentType", Schema = "dbo")]
    public class ApartmentType
    {
        public int TypeId { get; set; }
        public string Type { get; set; }

        public virtual List<Apartment> Apartments { get; set; }
    }
}
