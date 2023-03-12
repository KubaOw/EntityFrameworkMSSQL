using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Entities.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(com => com.CreatedDate).HasDefaultValueSql("getutcdate()");
            builder.Property(com => com.UpdatedDate).ValueGeneratedOnUpdate();
            builder.HasOne(com => com.Author)
            .WithMany(a => a.Comments)
            .HasForeignKey(com => com.AuthorId)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
