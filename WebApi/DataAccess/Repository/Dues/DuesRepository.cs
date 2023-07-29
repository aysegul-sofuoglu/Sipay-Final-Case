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
    public class DuesRepository : GenericRepository<Dues>, IDuesRepository
    {
        private readonly ManagementDbContext dbContext;
        public DuesRepository(ManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Dues> GetAll()
        {
            return dbContext.Set<Dues>().Include(x => x.Payments).ToList();
        }
    }
}
