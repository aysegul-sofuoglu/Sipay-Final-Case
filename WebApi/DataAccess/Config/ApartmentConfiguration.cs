using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Config
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        
        public void Configure(EntityTypeBuilder<Apartment> builder)
                {
            builder.Property(x => x.ApartmentId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Block).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Situation).IsRequired(true).HasDefaultValue(false);
            builder.Property(x => x.Type).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Floor).IsRequired(true);
            builder.Property(x => x.ApartmentNo).IsRequired(true);

            builder.HasIndex(x => x.UserId);


            builder.HasMany(x => x.Dueses).WithOne(x => x.Apartment).HasForeignKey(x => x.ApartmentId).IsRequired(true); ;
            builder.HasMany(x => x.Invoices).WithOne(x => x.Apartment).HasForeignKey(x => x.ApartmentId).IsRequired(true);
        }
    }
}
