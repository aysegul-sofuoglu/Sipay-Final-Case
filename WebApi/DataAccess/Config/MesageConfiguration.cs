using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Config
{
    public class MesageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.MessageId).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.SenderId).IsRequired(true);
            builder.Property(x => x.Mesage).IsRequired(true).HasMaxLength(300);
            builder.Property(x => x.Date).IsRequired(true);
            builder.Property(x => x.Seen).IsRequired(true);

            builder.HasIndex(x => x.SenderId);
        }
    }
}
