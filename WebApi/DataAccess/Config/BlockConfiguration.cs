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
    public class BlockConfiguration : IEntityTypeConfiguration<Block>
    {
    
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.Property(x => x.BlockId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(10);

            builder.HasKey(x => x.BlockId);

            builder.HasMany(x => x.Apartments).WithOne(x => x.Block).HasForeignKey(x => x.BlockId).IsRequired(true);
        }
    }
}
