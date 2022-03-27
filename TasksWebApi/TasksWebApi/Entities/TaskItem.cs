using System;

namespace TasksWebApi.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
