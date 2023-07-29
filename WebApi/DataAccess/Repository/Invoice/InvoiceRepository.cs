using DataAccess.Domain;
using DataAccess.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly ManagementDbContext dbContext;
        public InvoiceRepository(ManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Invoice> GetAll()
        {
            return dbContext.Set<Invoice>().Include(x => x.Payments).ToList();
        }
    }
}
