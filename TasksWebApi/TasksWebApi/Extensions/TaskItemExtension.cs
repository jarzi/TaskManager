using TasksWebApi.Dtos;
using TasksWebApi.Entities;

namespace TasksWebApi.Extensions
{
    public static class TaskItemExtension
    {
        public static TaskItemDto AsDto(this TaskItem taskItem)
        {
            return new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                Description = taskItem.Description,
                DeadlineDate = taskItem.DeadlineDate
            };
        }
    }
}
