using EntityFramework.ViewModels;
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
        public DbSet<WorkItemTag> WorkItemTags { get; set; }
        public DbSet<TopAuthors> ViewTopAuthors { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Email,x.Lastname});
        }code provides composite key for User table*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);//Assembly searches the project for classes that implement IEntityTypeConfiguration and calls the method Configure()

            modelBuilder.Entity<TopAuthors>(eb =>
            {
                eb.ToView("View_TopAuthors");
                eb.HasNoKey();
            });
        }
    }
}
