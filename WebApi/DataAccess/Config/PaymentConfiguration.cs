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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.PaymentId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CardId).IsRequired(true);
            //builder.Property(x => x.UserId).IsRequired(true);
           // builder.Property(x => x.DuesId).IsRequired(true);
            //builder.Property(x => x.InvoiceId).IsRequired(true);
            builder.Property(x => x.Date).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true).HasColumnType("decimal(18,2)");

            //builder.HasIndex(x => x.UserId);
            //builder.HasIndex(x => x.DuesId);
            // builder.HasIndex(x => x.InvoiceId);

            builder.HasOne(x => x.BankCardInfo)
           .WithMany(x => x.Payments)
           .HasForeignKey(x => x.CardId)
           .IsRequired(true).OnDelete(DeleteBehavior.NoAction);

           // builder.HasOne(x => x.Dues)
           //.WithMany(x => x.Payments)
           //.HasForeignKey(x => x.DuesId)
           //.IsRequired(true).OnDelete(DeleteBehavior.NoAction);

           // builder.HasOne(x => x.Invoice)
           //.WithMany(x => x.Payments)
           //.HasForeignKey(x => x.InvoiceId)
           //.IsRequired(true).OnDelete(DeleteBehavior.NoAction);

           // builder.HasOne(x => x.User)
           //.WithMany(x => x.Payments)
           //.HasForeignKey(x => x.UserId)
           //.IsRequired(true).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
