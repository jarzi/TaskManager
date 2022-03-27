using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TasksWebApi.Controllers;
using TasksWebApi.Dtos;
using TasksWebApi.Repositories;
using TestTasksWebApi.Helpers;
using TestTasksWebApi.Mock;
using Xunit;

namespace TestTasksWebApi
{
    public class TestTaskItemsController
    {
        private readonly ITaskItemsRepository _taskItemsRepository;
        private readonly TaskItemsController _controller;

        public TestTaskItemsController()
        {
            _taskItemsRepository = new TaskItemsFakeRepository();
            _controller = new TaskItemsController(_taskItemsRepository);
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsOkResult()
        {
            var actionResult = await _controller.GetTaskItems();
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsAllTaskItems()
        {
            var actionResult = await _controller.GetTaskItems();
            var resultObject = ActionResultValue.GetObjectResultContent(actionResult);
            Assert.Equal(_taskItemsRepository.GetTaskItemsAsync().Result.Count(), resultObject.Count());
        }

        [Fact]
        public async void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            var notFoundResult = await _controller.GetTaskItem(int.MinValue);
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public async void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            var testExistingId = _taskItemsRepository.GetTaskItemsAsync().Result.FirstOrDefault()?.Id;
            if (testExistingId.HasValue)
            {
                var okResult = await _controller.GetTaskItem(testExistingId.Value);
                Assert.IsType<OkObjectResult>(okResult.Result);
            }
        }

        [Fact]
        public async void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            var testExistingId = _taskItemsRepository.GetTaskItemsAsync().Result.FirstOrDefault()?.Id;
            if (testExistingId.HasValue)
            {
                var actionResult = await _controller.GetTaskItem(testExistingId.Value);
                var resultObject = ActionResultValue.GetObjectResultContent(actionResult);
                Assert.Equal(testExistingId, resultObject.Id);
            }
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var testCreateTaskItemDto = new CreateTaskItemDto
            {
                Title = "Test title",
                Description = "Test desc",
                DeadlineDate = System.DateTime.MinValue
            };
            var createdResponse = await _controller.PostTaskItem(testCreateTaskItemDto);
            Assert.IsType<CreatedAtActionResult>(createdResponse.Result);
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            var testCreateTaskItemDto = new CreateTaskItemDto
            {
                Title = "Test title",
                Description = "Test desc",
                DeadlineDate = System.DateTime.MinValue
            };
            var createdResponse = await _controller.PostTaskItem(testCreateTaskItemDto);
            dynamic resultObject = ActionResultValue.GetObjectResultContentDto(createdResponse);
            Assert.IsType<TaskItemDto>(resultObject);
            Assert.Equal("Test title", resultObject.Title);
            Assert.Equal("Test desc", resultObject.Description);
            Assert.Equal(System.DateTime.MinValue, resultObject.DeadlineDate);
        }

        [Fact]
        public void Add_InvalidObjectPassed_RequiredAttributes()
        {
            var testCreateTaskItemDto = new CreateTaskItemDto
            {
                Title = null,
                Description = null,
                DeadlineDate = System.DateTime.MaxValue
            };

            var result = Validator.TryValidateObject(testCreateTaskItemDto, new ValidationContext(testCreateTaskItemDto, null, null), null, true);
            Assert.False(result);
        }

        [Fact]
        public async void Remove_NotExistingIfPassed_ReturnsNotFoundResponse()
        {
            var notExistingId = int.MinValue;
            var badResponse = await _controller.DeleteTaskItem(notExistingId);
            Assert.IsType<NotFoundResult>(badResponse);
        }
        [Fact]
        public async void Remove_ExistingGuidPassed_ReturnsNoContentResultAndDelete()
        {
            var existingId = 1;
            var noContentResponse = await _controller.DeleteTaskItem(existingId);
            Assert.IsType<NoContentResult>(noContentResponse);
            var afterDelete = await _taskItemsRepository.GetTaskItemsAsync();
            Assert.Equal(3, afterDelete.Count());
        }

        [Fact]
        public async void Update_ExistingObjectPassed_ReturnedNoContentAndUpdateItem()
        {
            var testUpdateTaskItemDto = new UpdateTaskItemDto
            {
                Title = "Test title",
                Description = "Test desc",
                DeadlineDate = System.DateTime.MinValue
            };
            var noContentResponse = await _controller.UpdateTaskItem(2, testUpdateTaskItemDto);
            Assert.IsType<NoContentResult>(noContentResponse);
            Assert.Equal(2, _taskItemsRepository.GetTaskItemAsync(2).Result.Id);
            Assert.Equal(testUpdateTaskItemDto.Title, _taskItemsRepository.GetTaskItemAsync(2).Result.Title);
            Assert.Equal(testUpdateTaskItemDto.Description, _taskItemsRepository.GetTaskItemAsync(2).Result.Description);
            Assert.Equal(testUpdateTaskItemDto.DeadlineDate, _taskItemsRepository.GetTaskItemAsync(2).Result.DeadlineDate);
        }

        [Fact]
        public async void Update_ExistingObjectPassed_ReturnedNotFound()
        {
            var testUpdateTaskItemDto = new UpdateTaskItemDto
            {
                Title = "Test title",
                Description = "Test desc",
                DeadlineDate = System.DateTime.MinValue
            };
            var notFoundResponse = await _controller.UpdateTaskItem(int.MinValue, testUpdateTaskItemDto);
            Assert.IsType<NotFoundResult>(notFoundResponse);
        }
    }
}
