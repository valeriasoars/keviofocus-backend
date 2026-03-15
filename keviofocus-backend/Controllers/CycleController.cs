using keviofocus_backend.Enums;
using keviofocus_backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CycleController : ControllerBase
    {
        private readonly ICycleService _service;

        public CycleController(ICycleService service)
        {
            _service = service;
        }

        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetAllBySession(Guid sessionId)
        {
            var cycles = await _service.GetAllBySession(sessionId);
            return Ok(cycles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cycle = await _service.GetById(id);
            if (cycle is null) return NotFound();
            return Ok(cycle);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] CycleStatus status)
        {
            try
            {
                var cycle = await _service.UpdateStatus(id, status);
                if (cycle is null) return NotFound();
                return Ok(cycle);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       
    }
}
