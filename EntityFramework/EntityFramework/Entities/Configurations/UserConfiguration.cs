using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Adress)
                 .WithOne(a => a.User)
                 .HasForeignKey<Adress>(a => a.UserID);
            builder.HasIndex(u => new { u.Email, u.FullName });
        }
    }
}
