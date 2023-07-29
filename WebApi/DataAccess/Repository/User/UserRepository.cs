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
    public class UserRepository : GenericRepository<User>,  IUserRepository
    {
        private readonly ManagementDbContext dbContext;
        public UserRepository(ManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<User> GetAll()
        {
            return dbContext.Set<User>().Include(x=>x.BankCards)
                .Include(x=>x.Apartments).ThenInclude(x=>x.Dueses)
                .Include(x=>x.Messages)
                .Include(x => x.Payments)
                .ToList();
            //return dbContext.Set<User>().Include(x => x.Apartments).ToList();
            //return dbContext.Set<User>().Include(x => x.Messages).ToList();
            //return dbContext.Set<User>().Include(x => x.Payments).ToList();
        }
    }
}
