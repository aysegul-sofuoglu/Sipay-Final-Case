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
    public class ApartmentTypeConfiguration : IEntityTypeConfiguration<ApartmentType>
    {
        public void Configure(EntityTypeBuilder<ApartmentType> builder)
        {
            builder.Property(x => x.TypeId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.Type).IsRequired(true).HasMaxLength(10);

            builder.HasKey(x => x.TypeId);

            builder.HasMany(x => x.Apartments).WithOne(x => x.ApartmentType).HasForeignKey(x => x.TypeId).IsRequired(true);
        }
    }
}
