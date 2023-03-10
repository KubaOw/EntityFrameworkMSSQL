﻿namespace EntityFramework.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Value { get; set; }
        //public List<WorkItemTag> WorkItemTags { get; set; } = new List<WorkItemTag>();
        public List<WorkItem> WorkItems { get; set; }
    }
}
