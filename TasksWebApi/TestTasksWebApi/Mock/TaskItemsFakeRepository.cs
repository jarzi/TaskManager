using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebApi.Entities;
using TasksWebApi.Repositories;

namespace TestTasksWebApi.Mock
{
    internal class TaskItemsFakeRepository : ITaskItemsRepository
    {
        private readonly List<TaskItem> _taskItems;

        public TaskItemsFakeRepository()
        {
            _taskItems = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    Title = "First title",
                    Description = "First desc",
                    DeadlineDate = System.DateTime.Now
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Second title",
                    Description = "Second desc",
                    DeadlineDate = System.DateTime.Now.AddDays(2)
                },
                new TaskItem
                {
                    Id = 3,
                    Title = "Third title",
                    Description = "Third desc"
                },
                new TaskItem
                {
                    Id = 4,
                    Title = "Fourth title",
                    Description = "Fourth desc",
                    DeadlineDate = System.DateTime.MinValue
                }
            };
        }

        public async Task<int> CreateTaskItemAsync(TaskItem taskItem)
        {
            taskItem.Id = _taskItems.Max(x => x.Id) + 1;
            _taskItems.Add(taskItem);
            return await Task.FromResult(taskItem.Id);
        }

        public Task<bool> DeleteTaskItemAsync(int id)
        {
            var taskItem = _taskItems.FirstOrDefault(x => x.Id == id);

            if (taskItem == null)
            {
                return Task.FromResult(false);
            }

            _taskItems.Remove(taskItem);
            return Task.FromResult(true);
        }

        public async Task<TaskItem> GetTaskItemAsync(int id)
        {
            var taskItem = _taskItems.FirstOrDefault(x => x.Id == id);

            if (taskItem == null)
            {
                return null;
            }

            return await Task.FromResult(taskItem);
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            return await Task.FromResult(_taskItems.AsEnumerable());
        }

        public async Task<int?> UpdateTaskItemAsync(TaskItem taskItem)
        {
            var updateTaskItem = _taskItems.FirstOrDefault(x => x.Id == taskItem.Id);

            if (updateTaskItem == null)
            {
                return null;
            }

            updateTaskItem.Title = taskItem.Title;
            updateTaskItem.Description = taskItem.Description;
            updateTaskItem.DeadlineDate = taskItem.DeadlineDate;

            return await Task.FromResult(new int?(updateTaskItem.Id));
        }
    }
}
