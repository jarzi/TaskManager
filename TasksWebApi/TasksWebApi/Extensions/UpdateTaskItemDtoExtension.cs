using TasksWebApi.Dtos;
using TasksWebApi.Entities;

namespace TasksWebApi.Extensions
{
    public static class UpdateTaskItemDtoExtension
    {
        public static TaskItem AsEntity(this UpdateTaskItemDto UpdateTaskItemDto, TaskItem taskItem)
        {
            taskItem.Title = UpdateTaskItemDto.Title;
            taskItem.Description = UpdateTaskItemDto.Description;
            taskItem.DeadlineDate = UpdateTaskItemDto.DeadlineDate;

            return taskItem;
        }
    }
}
