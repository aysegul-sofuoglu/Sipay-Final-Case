using DataAccess.Domain;
using DataAccess.Repository;
using Serilog;

namespace DataAccess.Uow
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ManagementDbContext dbContext;

        public UnitOfWork(ManagementDbContext dbContext)
        {
            this.dbContext = dbContext;

            ApartmentRepository = new GenericRepository<Apartment>(dbContext);
            BankCardInfoRepository = new GenericRepository<BankCardInfo>(dbContext);
            DuesRepository = new GenericRepository<Dues>(dbContext);
            GenreRepository = new GenericRepository<Genre>(dbContext);
            InvoiceRepository = new GenericRepository<Invoice>(dbContext);
            MessageRepository = new GenericRepository<Message>(dbContext);
            PaymentRepository = new GenericRepository<Payment>(dbContext);
            UserRepository = new GenericRepository<User>(dbContext);
            UserLogRepository = new GenericRepository<UserLog>(dbContext);
        }

        public void Complete()
        {
            dbContext.SaveChanges();
        }

        void IUnitOfWork.CompleteWithTransaction()
        {
            using(var dbTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.SaveChanges();
                    dbTransaction.Commit(); 
                }
                catch(Exception ex)
                {
                    dbTransaction.Rollback();
                    Log.Error(ex, "CompleteWithTransaction");
                }
            }
        }

     

        public IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : class {

            return new GenericRepository<Entity>(dbContext);
        }

        public IGenericRepository<Apartment> ApartmentRepository { get; private set; }

        public IGenericRepository<BankCardInfo> BankCardInfoRepository { get; private set; }

        public IGenericRepository<Dues> DuesRepository { get; private set; }

        public IGenericRepository<Genre> GenreRepository { get; private set; }

        public IGenericRepository<Invoice> InvoiceRepository { get; private set; }

        public IGenericRepository<Message> MessageRepository { get; private set; }

        public IGenericRepository<Payment> PaymentRepository { get; private set; }

        public IGenericRepository<User> UserRepository { get; private set; }
        public IGenericRepository<UserLog> UserLogRepository { get; private set; }

       
    }
}
