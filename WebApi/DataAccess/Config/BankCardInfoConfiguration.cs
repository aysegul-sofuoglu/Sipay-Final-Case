﻿using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Config
{
    public class BankCardInfoConfiguration : IEntityTypeConfiguration<BankCardInfo>
    {
        public void Configure(EntityTypeBuilder<BankCardInfo> builder)
        {
            builder.Property(x => x.CardId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Balance).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.CardNo).IsRequired(true).HasMaxLength(20);

            builder.HasKey(x => x.CardId);
            builder.HasIndex(x => x.UserId);

            builder.HasMany(x => x.Payments).WithOne(x => x.BankCardInfo).HasForeignKey(x => x.CardId).IsRequired(true);

        }
    }
}
