using keviofocus_backend.Data;
using keviofocus_backend.DTOs;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Services
{
    public class SessionService : ISessionService
    {
        private readonly KevioDbContext _context;

        public SessionService(KevioDbContext context)
        {
            _context = context;
        }

        public async Task<SessionModel> CreateSessionAsync(SessionCreateDto dto)
        {
            var session = new SessionModel
            {
                Name = dto.Name,
                Description = dto.Description,
                FocusDurationMinutes = dto.FocusDurationMinutes,
                BreakDurationMinutes = dto.BreakDurationMinutes,
                Cycles = dto.Cycles,
                Color = dto.Color,
                Icon = dto.Icon,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task<IEnumerable<SessionModel>> GetAllSessionsAsync()
        {
            return await _context.Sessions
           .Include(s => s.Tasks)
           .ToListAsync();
        }

        public async Task<SessionModel?> GetSessionByIdAsync(string id)
        {
            return await _context.Sessions
            .Include(s => s.Tasks)
            .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
