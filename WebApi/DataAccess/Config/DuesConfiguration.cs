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
            //builder.HasMany(x => x.Invoices).WithOne(x => x.Dues).HasForeignKey(x => x.DuesId).IsRequired(true);

           
        }
    }
}
