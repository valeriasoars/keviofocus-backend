using keviofocus_backend.Dto.FocusTask;
using keviofocus_backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FocusTaskController : ControllerBase
    {

        private readonly IFocusTaskService _service;

        public FocusTaskController(IFocusTaskService service)
        {
            _service = service;
        }

        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetAllBySession(Guid sessionId)
        {
            var tasks = await _service.GetAllBySession(sessionId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _service.GetById(id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FocusTaskCreateDto dto)
        {
            var task = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FocusTaskUpdateDto dto)
        {
            try
            {
                var task = await _service.Update(id, dto);
                if (task is null) return NotFound();
                return Ok(task);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var task = await _service.Complete(id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
