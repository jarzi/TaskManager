using System;
using System.ComponentModel.DataAnnotations;

namespace TasksWebApi.Dtos
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
