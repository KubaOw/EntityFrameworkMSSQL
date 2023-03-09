﻿namespace EntityFramework.Entities
{
    public class WorkItem
    {
        public string State { get; set; }
        public string Area { get; set; }
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        //Epic
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        //Issue
        public decimal Efford { get; set; }
        //Task
        public string Activity { get; set; }
        public decimal RemainingWork { get; set; }
        public string Type { get; set; }
    }
}
