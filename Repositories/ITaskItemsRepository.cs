using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksWebApi.Entities;

namespace TasksWebApi.Repositories
{
    public interface ITaskItemsRepository
    {
        public Task<int> CreateTaskItemAsync(TaskItem taskItem);
        public Task<TaskItem> GetTaskItemAsync(int id);
        public Task<IEnumerable<TaskItem>> GetTaskItemsAsync();
        public Task<int?> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<bool> DeleteTaskItemAsync(int id);
    }
}
