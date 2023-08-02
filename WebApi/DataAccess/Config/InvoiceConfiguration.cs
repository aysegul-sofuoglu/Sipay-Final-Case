using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Config
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.InvoiceId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.GenreId).IsRequired(true);
            builder.Property(x => x.ApartmentId).IsRequired(true);
            builder.Property(x => x.Mounth).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Year).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true);

            builder.HasIndex(x => x.GenreId);
            builder.HasIndex(x => x.ApartmentId);

            builder.HasMany(x => x.Payments).WithOne(x => x.Invoice).HasForeignKey(x => x.InvoiceId).IsRequired(true);
        }
    }
}
