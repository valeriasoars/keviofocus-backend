using keviofocus_backend.DTOs;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService) => _taskService = taskService;

        [HttpPost]
        public async Task<ActionResult<TaskItemModel>> Create(TaskCreateDto dto)
        {
            var task = await _taskService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetBySession), new { sessionId = task.SessionId }, task);
        }

        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<IEnumerable<TaskItemModel>>> GetBySession(string sessionId)
        {
            return Ok(await _taskService.GetTasksBySessionIdAsync(sessionId));
        }

        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult<TaskItemModel>> Toggle(string id)
        {
            var task = await _taskService.ToggleTaskStatusAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Task not found" });
            }

            return NoContent();
        }
    }
}
