using Business.Generic;
using DataAccess.Domain;
using Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IDuesService : IGenericService<Dues, DuesRequest, DuesResponse>
    {
    }
}
