using keviofocus_backend.DTOs;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCompletionsController : ControllerBase
    {
        private readonly ITaskCompletionService _completionService;

        public TaskCompletionsController(ITaskCompletionService completionService)
            => _completionService = completionService;

        [HttpPost]
        public async Task<ActionResult<TaskCompletionModel>> Complete(TaskCompletionDto dto)
        {
            var completion = await _completionService.CompleteTaskAsync(dto);
            return Ok(completion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Undo(string id)
        {
            var result = await _completionService.UndoTaskCompletionAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("run/{runId}")]
        public async Task<ActionResult<IEnumerable<TaskCompletionModel>>> GetByRun(string runId)
        {
            return Ok(await _completionService.GetCompletionsByRunIdAsync(runId));
        }
    }
}
