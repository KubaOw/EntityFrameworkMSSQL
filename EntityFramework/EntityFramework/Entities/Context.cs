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
        public DbSet<WorkItemState> WorkItemStates { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Email,x.Lastname});
        }code provides composite key for User table*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItemState>()
                .Property(wis => wis.Value)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<WorkItemState>()
               .HasData(new WorkItemState() { Id = 1, Value = "To Do" },
               new WorkItemState() { Id = 2, Value = "Doing" },
               new WorkItemState() { Id = 3, Value = "Done" });

            modelBuilder.Entity<WorkItem>(eb =>
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

            modelBuilder.Entity<Epic>().Property(e => e.EndDate).HasPrecision(3);
            modelBuilder.Entity<Task>().Property(t => t.Activity).HasMaxLength(200);
            modelBuilder.Entity<Task>().Property(t => t.RemainingWork).HasPrecision(14,2);
            modelBuilder.Entity<Issue>().Property(i => i.Efford).HasColumnType("decimal(5,1)");

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(com => com.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(com => com.UpdatedDate).ValueGeneratedOnUpdate();
                eb.HasOne(com => com.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<User>()
                .HasOne(u => u.Adress)
                .WithOne(a => a.User)
                .HasForeignKey<Adress>(a => a.UserID);

            
        }
    }
}
