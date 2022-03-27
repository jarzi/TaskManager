using Microsoft.AspNetCore.Mvc;
using TasksWebApi.Dtos;
using TasksWebApi.Entities;
using TasksWebApi.Extensions;

namespace TestTasksWebApi.Helpers
{
    internal class ActionResultValue
    {
        public static T GetObjectResultContent<T>(ActionResult<T> result)
        {
            return (T)((ObjectResult)result.Result).Value;
        }

        public static object GetObjectResultContentDto(ActionResult<TaskItem> result)
        {
            return ((ObjectResult)result.Result).Value;
        }
    }
}
