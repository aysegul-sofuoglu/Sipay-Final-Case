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
    public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
    {
        private readonly ManagementDbContext dbContext;
        public ApartmentRepository(ManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Apartment> GetAll()
        {
            return dbContext.Set<Apartment>().Include(x => x.Dueses).ThenInclude(x=>x.Payments)
                .Include(x => x.Invoices).ThenInclude(x=>x.Payments).ToList();
            //return dbContext.Set<User>().Include(x => x.Apartments).ToList();
            //return dbContext.Set<User>().Include(x => x.Messages).ToList();
            //return dbContext.Set<User>().Include(x => x.Payments).ToList();
        }
    }
}
