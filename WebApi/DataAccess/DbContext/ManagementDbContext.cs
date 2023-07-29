using DataAccess.Config;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ManagementDbContext : DbContext
    {
        public ManagementDbContext(DbContextOptions<ManagementDbContext> options) : base(options)
        {
        }

        //dbSet
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<BankCardInfo> BankCardInfos { get; set; }
        public DbSet<Dues> Dueses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
            modelBuilder.ApplyConfiguration(new BankCardInfoConfiguration());
            modelBuilder.ApplyConfiguration(new DuesConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new MesageConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
