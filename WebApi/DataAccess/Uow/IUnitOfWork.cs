using DataAccess.Domain;
using DataAccess.Repository;

namespace DataAccess.Uow
{
    public interface IUnitOfWork
    {
        void Complete(); //save
        void CompleteWithTransaction();

        IGenericRepository<Entity> DynamicRepository<Entity>() where Entity : class;
        IGenericRepository<Apartment> ApartmentRepository { get; }
        IGenericRepository<BankCardInfo> BankCardInfoRepository { get; }
        IGenericRepository<Dues> DuesRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }
        IGenericRepository<Invoice> InvoiceRepository { get; }
        IGenericRepository<Message> MessageRepository { get; }
        IGenericRepository<Payment> PaymentRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<UserLog> UserLogRepository { get; }
        IGenericRepository<Block> BlockRepository { get; }
        IGenericRepository<ApartmentType> ApartmentTypeRepository { get; }



    }
}
