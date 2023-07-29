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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(x => x.GenreId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(true);

            
            builder.HasMany(x => x.Invoices).WithOne(x => x.Genre).HasForeignKey(x => x.GenreId).IsRequired(true);
        }
    }
}
