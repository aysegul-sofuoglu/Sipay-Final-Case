using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Config
{
    public class DuesConfiguration : IEntityTypeConfiguration<Dues>
    {
        public void Configure(EntityTypeBuilder<Dues> builder)
        {
            builder.Property(x => x.DuesId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.ApartmentId).IsRequired(true);
            builder.Property(x => x.Mounth).IsRequired(true);
            builder.Property(x => x.Year).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true).HasColumnType("decimal(18,2)");

            builder.HasIndex(x => x.ApartmentId);


            builder.HasMany(x => x.Payments).WithOne(x => x.Dues).HasForeignKey(x => x.DuesId).IsRequired(true); 

           
        }
    }
}
