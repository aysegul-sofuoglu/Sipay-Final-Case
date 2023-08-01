using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Entity GetByIdWithInclude(int id, params string[] includes)
        {
            var query = dbContext.Set<Entity>().AsQueryable();
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));
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
