using keviofocus_backend.DTOs;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionModel>>> GetAll()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionModel>> GetById(string id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);

            if (session == null)
                return NotFound(new { message = "Session not found" });

            return Ok(session);
        }

        [HttpPost]
        public async Task<ActionResult<SessionModel>> Create(SessionCreateDto dto)
        {
            try
            {
                var session = await _sessionService.CreateSessionAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error creating session", details = ex.Message });
            }
        }

    }
}
