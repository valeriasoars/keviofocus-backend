using keviofocus_backend.DTOs;
using keviofocus_backend.Emuns;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionRunsController : ControllerBase
    {
        private readonly ISessionRunService _runService;

        public SessionRunsController(ISessionRunService runService) => _runService = runService;

        [HttpPost]
        public async Task<ActionResult<SessionRunModel>> Start(SessionRunCreateDto dto)
        {
            var run = await _runService.StartRunAsync(dto);
            return CreatedAtAction(nameof(GetHistory), new { sessionId = run.SessionId }, run);
        }

        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<IEnumerable<SessionRunModel>>> GetHistory(string sessionId)
        {
            return Ok(await _runService.GetHistoryBySessionIdAsync(sessionId));
        }


        [HttpPatch("{id}/update-metrics")]
        public async Task<ActionResult<SessionRunModel>> UpdateMetrics(string id, StatusEnum status, int focusSeconds, int breakSeconds)
        {
            var run = await _runService.UpdateStatusAsync(id, status, focusSeconds, breakSeconds);
            return run == null ? NotFound() : Ok(run);
        }

        [HttpPatch("{id}/finish")]
        public async Task<ActionResult<SessionRunModel>> Finish(string id)
        {
            var run = await _runService.FinishRunAsync(id);
            return run == null ? NotFound() : Ok(run);
        }
    }
}
