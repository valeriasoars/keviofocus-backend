using keviofocus_backend.Data;
using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;
using keviofocus_backend.Models;
using keviofocus_backend.Interfaces;


using Microsoft.EntityFrameworkCore;


namespace keviofocus_backend.Service
{
    public class SessionService : ISessionService
    {
        private readonly AppDbContext _context;

        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SessionResponseDto>> GetAll()
        {
            return await _context.Sessions
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => ToResponse(s))
                .ToListAsync();
        }

        public async Task<SessionResponseDto?> GetById(Guid id) 
        {
            var session = await _context.Sessions.FindAsync(id);
            return session is null ? null : ToResponse(session);
        }

        public async Task<SessionResponseDto> Create(SessionCreateDto dto)
        {
            var session = new Session
            {
                Name = dto.Name,
                Description = dto.Description,
                FocusDurationMin = dto.FocusDurationMin,
                ShortBreakDurationMin = dto.ShortBreakDurationMin,
                LongBreakDurationMin = dto.LongBreakDurationMin,
                CyclesBeforeLongBreak = dto.CyclesBeforeLongBreak,
                TotalCycles = dto.TotalCycles,
                Status = SessionStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return ToResponse(session);
        }

        public async Task<SessionResponseDto?> Update(Guid id, SessionUpdateDto dto)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session is null) return null;

            if (session.Status == SessionStatus.InProgress)
                throw new InvalidOperationException("Cannot update a session that is in progress.");

            if (dto.Name is not null) session.Name = dto.Name;
            if (dto.Description is not null) session.Description = dto.Description;
            if (dto.FocusDurationMin is not null) session.FocusDurationMin = dto.FocusDurationMin.Value;
            if (dto.ShortBreakDurationMin is not null) session.ShortBreakDurationMin = dto.ShortBreakDurationMin.Value;
            if (dto.LongBreakDurationMin is not null) session.LongBreakDurationMin = dto.LongBreakDurationMin.Value;
            if (dto.CyclesBeforeLongBreak is not null) session.CyclesBeforeLongBreak = dto.CyclesBeforeLongBreak.Value;
            if (dto.TotalCycles is not null) session.TotalCycles = dto.TotalCycles.Value;

            await _context.SaveChangesAsync();
            return ToResponse(session);

        }

        public async Task<bool> Delete(Guid id) 
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session is null) return false;

            // Só permite excluir se estiver pendente
            if (session.Status != SessionStatus.Pending)
                throw new InvalidOperationException("Only pending sessions can be deleted.");

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<SessionResponseDto?> Start(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session is null) return null;

            if (session.Status != SessionStatus.Pending)
                throw new InvalidOperationException("Only pending sessions can be started.");

            // Cria os ciclos automaticamente
            for (int i = 1; i <= session.TotalCycles; i++)
            {
                _context.Cycles.Add(new Cycle
                {
                    SessionId = session.Id,
                    Number = i,
                    Status = CycleStatus.Pending
                });
            }

            session.Status = SessionStatus.InProgress;
            session.StartedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ToResponse(session);
        }

        public async Task<SessionResponseDto?> Pause(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session is null) return null;

            if (session.Status != SessionStatus.InProgress)
                throw new InvalidOperationException("Only in-progress sessions can be paused.");

            session.Status = SessionStatus.Paused;
            await _context.SaveChangesAsync();
            return ToResponse(session);
        }

        public async Task<SessionResponseDto?> Resume(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session is null) return null;

            if (session.Status != SessionStatus.Paused)
                throw new InvalidOperationException("Only paused sessions can be resumed.");

            session.Status = SessionStatus.InProgress;
            await _context.SaveChangesAsync();
            return ToResponse(session);
        }

        public async Task<SessionResponseDto?> Finish(Guid id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session is null) return null;

            if (session.Status == SessionStatus.Completed)
                throw new InvalidOperationException("Session is already completed.");

            session.Status = SessionStatus.Completed;
            session.FinishedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return ToResponse(session);
        }



        public static SessionResponseDto ToResponse(Session session) => new()
        {
            Id = session.Id,
            Name = session.Name,
            Description = session.Description,
            FocusDurationMin = session.FocusDurationMin,
            ShortBreakDurationMin = session.ShortBreakDurationMin,
            LongBreakDurationMin = session.LongBreakDurationMin,
            CyclesBeforeLongBreak = session.CyclesBeforeLongBreak,
            TotalCycles = session.TotalCycles,
            CompletedCycles = session.CompletedCycles,
            Status = session.Status,
            CreatedAt = session.CreatedAt,
            StartedAt = session.StartedAt,
            FinishedAt = session.FinishedAt

        };








    }
}
