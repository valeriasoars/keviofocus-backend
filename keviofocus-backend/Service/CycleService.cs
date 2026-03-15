using keviofocus_backend.Data;
using keviofocus_backend.Dto.Cycle;
using keviofocus_backend.Enums;
using keviofocus_backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Service
{
    public class CycleService : ICycleService
    {
        private readonly AppDbContext _context;

        public CycleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CycleResponseDto>> GetAllBySession(Guid sessionId)
        {
            var cycles = await _context.Cycles
                .Where(c => c.SessionId == sessionId)
                .OrderBy(c => c.Number)
                .ToListAsync();

            return cycles.Select(c => ToResponse(c)).ToList();
        }

        public async Task<CycleResponseDto?> GetById(Guid id)
        {
            var cycle = await _context.Cycles.FindAsync(id);
            return cycle is null ? null : ToResponse(cycle);
        }


        public async Task<CycleResponseDto?> UpdateStatus(Guid id, CycleStatus status)
        {
            var cycle = await _context.Cycles.FindAsync(id);
            if (cycle is null) return null;

            cycle.Status = status;

            // Atualiza timestamps conforme o status
            switch (status)
            {
                case CycleStatus.InFocus:
                    cycle.FocusStart = DateTime.UtcNow;
                    break;
                case CycleStatus.OnBreak:
                    cycle.FocusEnd = DateTime.UtcNow;
                    cycle.BreakStart = DateTime.UtcNow;
                    if (cycle.FocusStart.HasValue)
                        cycle.FocusElapsedSeconds = (int)(DateTime.UtcNow - cycle.FocusStart.Value).TotalSeconds;
                    break;
                case CycleStatus.Completed:
                    cycle.BreakEnd = DateTime.UtcNow;
                    break;
                case CycleStatus.Interrupted:
                    cycle.FocusEnd = DateTime.UtcNow;
                    if (cycle.FocusStart.HasValue)
                        cycle.FocusElapsedSeconds = (int)(DateTime.UtcNow - cycle.FocusStart.Value).TotalSeconds;
                    break;
            }

            await _context.SaveChangesAsync();
            return ToResponse(cycle);
        }





        public static CycleResponseDto ToResponse(Models.Cycle c) => new()
        {
            Id = c.Id,
            SessionId = c.SessionId,
            Number = c.Number,
            Status = c.Status,
            FocusStart = c.FocusStart,
            FocusEnd = c.FocusEnd,
            BreakStart = c.BreakStart,
            BreakEnd = c.BreakEnd,
            FocusElapsedSeconds = c.FocusElapsedSeconds
        };
    }
}
