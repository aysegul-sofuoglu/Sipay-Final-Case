using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ManagementDbContext dbContext;
        public GenericRepository(ManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Delete(Entity entity)
        {
            dbContext.Set<Entity>().Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = dbContext.Set<Entity>().Find(id);
            Delete(entity);
        }

        public List<Entity> GetAll()
        {
            return dbContext.Set<Entity>().ToList();
        }

        public IQueryable<Entity> GetAllAsQueryable()
        {
            return dbContext.Set<Entity>().AsQueryable();
        }

        public Entity GetById(int id)
        {
            var entity = dbContext.Set<Entity>().Find(id);
            return entity;
        }

        public void Insert(Entity entity)
        {

            dbContext.Set<Entity>().Add(entity);
        }

        public void Update(Entity entity)
        {
            dbContext.Set<Entity>().Update(entity);
        }

        public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
        {
            return dbContext.Set<Entity>().Where(expression).AsQueryable();
        }


        public Entity GetByIdWithInclude(int id, params string[] includes)
        {
            var query = dbContext.Set<Entity>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
         

            if(typeof(Entity) == typeof(User))
            {
                query = query.Where(x => (x as User).UserId == id);
            }
            else if (typeof(Entity) == typeof(Apartment))
            {
                query = query.Where(x => (x as Apartment).ApartmentId == id);
            }
            else if (typeof(Entity) == typeof(BankCardInfo))
            {
                query = query.Where(x => (x as BankCardInfo).CardId == id);
            }
            else if (typeof(Entity) == typeof(Dues))
            {
                query = query.Where(x => (x as Dues).DuesId == id);
            }
            else if (typeof(Entity) == typeof(Invoice))
            {
                query = query.Where(x => (x as Invoice).InvoiceId == id);
            }
            else if (typeof (Entity) == typeof(Message))
            {
                query = query.Where(x => (x as Message).MessageId == id);
            }
            else if (typeof(Entity) == typeof(Payment))
            {
                query = query.Where(x => (x as Payment).PaymentId == id);
            }
            else if (typeof(Entity) == typeof(Genre))
            {
                query = query.Where(x => (x as Genre).GenreId == id);
            }
            else if (typeof(Entity) == typeof(Block))
            {
                query = query.Where(x => (x as Block).BlockId == id);
            }
            else if (typeof(Entity) == typeof(ApartmentType))
            {
                query = query.Where(x => (x as ApartmentType).TypeId == id);
            }

            return query.FirstOrDefault();
        }

        public List <Entity> GetAllWithInclude(params string[] includes)
        {
            var query = dbContext.Set<Entity>().AsQueryable();
            query = includes.Aggregate(query, (currenct, inc) => currenct.Include(inc));
            return query.ToList();
        }
    }
}
