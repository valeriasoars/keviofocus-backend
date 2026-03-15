using keviofocus_backend.Dto.Session;
using keviofocus_backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;
        public SessionController(ISessionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sessions = await _service.GetAll();
            return Ok(sessions);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var session = await _service.GetById(id);
            if (session is null) return NotFound();
            return Ok(session);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionCreateDto dto)
        {
            var session = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new {id = session.Id}, session);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SessionUpdateDto dto)
        {
            try
            {
                var session = await _service.Update(id, dto);
                if (session is null) return NotFound();
                return Ok(session);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.Delete(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("{id}/start")]
        public async Task<IActionResult> Start(Guid id)
        {
            try
            {
                var session = await _service.Start(id);
                if (session is null) return NotFound();
                return Ok(session);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/pause")]
        public async Task<IActionResult> Pause(Guid id)
        {
            try
            {
                var session = await _service.Pause(id);
                if (session is null) return NotFound();
                return Ok(session);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/resume")]
        public async Task<IActionResult> Resume(Guid id)
        {
            try
            {
                var session = await _service.Resume(id);
                if (session is null) return NotFound();
                return Ok(session);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/finish")]
        public async Task<IActionResult> Finish(Guid id)
        {
            try
            {
                var session = await _service.Finish(id);
                if (session is null) return NotFound();
                return Ok(session);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
