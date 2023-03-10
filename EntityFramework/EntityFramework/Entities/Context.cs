using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Entities
{
    public class Context: DbContext//class which represents database
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Email,x.Lastname});
        }code provides composite key for User table*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.State).IsRequired();
                eb.Property(wi => wi.Area).HasColumnType("varchar(200)");
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.Efford).HasColumnType("decimal(5,1)");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Activity).HasMaxLength(200);
                eb.Property(wi => wi.RemainingWork).HasPrecision(14, 2);
                eb.Property(wi => wi.Priority).HasDefaultValue(1);
                eb.HasMany(wi => wi.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);

                eb.HasOne(wi => wi.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(wi => wi.AuthorId);

                eb.HasMany(wi => wi.Tags)
                .WithMany(t => t.WorkItems)
                .UsingEntity<WorkItemTag>(
                    wi=>wi.HasOne(wit => wit.Tag)
                    .WithMany()
                    .HasForeignKey(wit=>wit.TagId),

                    wi => wi.HasOne(wit => wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(wit => wit.WorkItemID),

                    wit=>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemID });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    });
            });
            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(com => com.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(com => com.UpdatedDate).ValueGeneratedOnUpdate();
            });
            modelBuilder.Entity<User>()
                .HasOne(u => u.Adress)
                .WithOne(a => a.User)
                .HasForeignKey<Adress>(a => a.UserID);

            
        }
    }
}
