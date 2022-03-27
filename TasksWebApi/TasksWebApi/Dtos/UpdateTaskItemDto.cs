using System;
using System.ComponentModel.DataAnnotations;

namespace TasksWebApi.Dtos
{
    public class UpdateTaskItemDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
