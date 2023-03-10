namespace EntityFramework.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Adress Adress { get; set; }
        public List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
    }
}
