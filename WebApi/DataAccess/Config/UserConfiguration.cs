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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.TCNo).IsRequired(true).HasMaxLength(11);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Telephone).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.PlateNo).IsRequired(false).HasMaxLength(20);

            builder.HasMany(x=>x.BankCards).WithOne(x=>x.User).HasForeignKey(x=>x.UserId).IsRequired(true); ;
            builder.HasMany(x => x.Messages).WithOne(x => x.User).HasForeignKey(x => x.SenderId).IsRequired(true);
            builder.HasMany(x => x.Payments).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired(true); ;
            builder.HasMany(x => x.Apartments).WithOne(x => x.User).HasForeignKey(x => x.UserId).IsRequired(true); ;

        }
    }
}
