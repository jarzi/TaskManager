using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebApi.Entities;
using TasksWebApi.Extensions;

namespace TasksWebApi.Repositories
{
    public class TaskItemsRepository : ITaskItemsRepository
    {
        private readonly TaskItemsContext _taskItemsContext;

        public TaskItemsRepository(TaskItemsContext taskContext)
        {
            _taskItemsContext = taskContext;
        }

        public async Task<int> CreateTaskItemAsync(TaskItem taskItem)
        {
            _taskItemsContext.TaskItems.Add(taskItem);
            return await _taskItemsContext.SaveChangesAsync();
        }

        public async Task<TaskItem> GetTaskItemAsync(int id)
        {
            return await _taskItemsContext.TaskItems.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetTaskItemsAsync()
        {
            return await _taskItemsContext.TaskItems.ToListAsync();
        }

        public async Task<int?> UpdateTaskItemAsync(TaskItem taskItem)
        {
            int? id = null;
            _taskItemsContext.TaskItems.Update(taskItem);
            try
            {
                id = await _taskItemsContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_taskItemsContext.TaskItems.Any(t => t.Id == taskItem.Id))
            {
                return null;
            }

            return id;
        }

        public async Task<bool> DeleteTaskItemAsync(int id)
        {
            var taskItem = await _taskItemsContext.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return false;
            }

            _taskItemsContext.TaskItems.Remove(taskItem);
            await _taskItemsContext.SaveChangesAsync();
            return true;
        }
    }
}
