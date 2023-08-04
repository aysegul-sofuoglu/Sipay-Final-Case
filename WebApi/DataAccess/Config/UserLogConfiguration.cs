using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Config
{
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Date).IsRequired(true);
            builder.Property(x => x.LogType).IsRequired(true).HasMaxLength(20);
        }
    }
}
