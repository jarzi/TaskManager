using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebApi.Dtos;
using TasksWebApi.Entities;
using TasksWebApi.Extensions;
using TasksWebApi.Repositories;

namespace TasksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskItemsRepository _taskItemsRepository;

        public TaskItemsController(ITaskItemsRepository taskItemsRepository)
        {
            _taskItemsRepository = taskItemsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(CreateTaskItemDto createTaskItemDto)
        {
            var taskItem = new TaskItem
            {
                Title = createTaskItemDto.Title,
                Description = createTaskItemDto.Description,
                DeadlineDate = createTaskItemDto.DeadlineDate
            };

            var id = await _taskItemsRepository.CreateTaskItemAsync(taskItem);

            return CreatedAtAction(nameof(GetTaskItem), new { id }, taskItem.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskItem(int id)
        {
            var taskItem = await _taskItemsRepository.GetTaskItemAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(taskItem.AsDto());
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskItemDto>>> GetTaskItems()
        {
            var taskItems = await _taskItemsRepository.GetTaskItemsAsync();
            return Ok(taskItems.Select(t => t.AsDto()).ToList());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, UpdateTaskItemDto updateTaskItemDto)
        {
            var taskItem = await _taskItemsRepository.GetTaskItemAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem = updateTaskItemDto.AsEntity(taskItem);

            var updatedTaskItemId = await _taskItemsRepository.UpdateTaskItemAsync(taskItem);
            if (!updatedTaskItemId.HasValue)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var success = await _taskItemsRepository.DeleteTaskItemAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
