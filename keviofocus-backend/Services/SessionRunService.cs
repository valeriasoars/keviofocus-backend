using keviofocus_backend.Data;
using keviofocus_backend.DTOs;
using keviofocus_backend.Emuns;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Services
{
    public class SessionRunService : ISessionRunService
    {
        private readonly KevioDbContext _context;

        public SessionRunService(KevioDbContext context) => _context = context;

        public async Task<SessionRunModel> StartRunAsync(SessionRunCreateDto dto)
        {
            var run = new SessionRunModel
            {
                SessionId = dto.SessionId,
                Status = StatusEnum.running,
                CurrentCycle = 1,
                StartedAt = DateTime.UtcNow
            };

            _context.SessionRuns.Add(run);
            await _context.SaveChangesAsync();
            return run;
        }

        public async Task<SessionRunModel?> UpdateStatusAsync(string runId, StatusEnum status, int focusSeconds, int breakSeconds)
        {
            var run = await _context.SessionRuns.FindAsync(runId);
            if (run == null) return null;

            run.Status = status;
            run.TotalFocusSeconds += focusSeconds;
            run.TotalBreakSeconds += breakSeconds;

            await _context.SaveChangesAsync();
            return run;
        }

        public async Task<SessionRunModel?> FinishRunAsync(string runId)
        {
            var run = await _context.SessionRuns.FindAsync(runId);
            if (run == null) return null;

            run.Status = StatusEnum.completed;
            run.FinishedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return run;
        }

        public async Task<IEnumerable<SessionRunModel>> GetHistoryBySessionIdAsync(string sessionId)
        {
           return await _context.SessionRuns
          .Where(r => r.SessionId == sessionId)
          .OrderByDescending(r => r.StartedAt)
          .ToListAsync<SessionRunModel>();
        }


    }
}
