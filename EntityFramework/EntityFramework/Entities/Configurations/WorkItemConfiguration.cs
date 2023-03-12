using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Entities.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> eb)
        {
            eb.HasOne(wi => wi.State)
                .WithMany()
                .HasForeignKey(wi => wi.StateID);
            eb.Property(wi => wi.Area).HasColumnType("varchar(200)");
            eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
            eb.Property(wi => wi.Priority).HasDefaultValue(1);
            eb.HasMany(wi => wi.Comments)
            .WithOne(c => c.WorkItem)
            .HasForeignKey(c => c.WorkItemId);

            eb.HasOne(wi => wi.Author)
            .WithMany(u => u.WorkItems)
            .HasForeignKey(wi => wi.AuthorId);

            eb.HasMany(w => w.Tags)
            .WithMany(t => t.WorkItems)
            .UsingEntity<WorkItemTag>(
                w => w.HasOne(wiT => wiT.Tag)
                .WithMany()
                .HasForeignKey(wiT => wiT.TagId),

                w => w.HasOne(wiT => wiT.WorkItem)
                .WithMany()
                .HasForeignKey(wiT => wiT.WorkItemID),

                wiT =>
                {
                    wiT.HasKey(x => new { x.TagId, x.WorkItemID });
                    wiT.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                });
        }
    }
}
