using keviofocus_backend.Data;
using keviofocus_backend.Dto.FocusTask;
using keviofocus_backend.Interfaces;
using keviofocus_backend.Models;
using TaskStatus = keviofocus_backend.Enums.TaskStatus;

using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Service
{
    public class FocusTaskService : IFocusTaskService
    {
        private readonly AppDbContext _context;

        public  FocusTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FocusTaskResponseDto>> GetAllBySession(Guid sessionId)
        {
           var tasks = await _context.Tasks
                .Where(t => t.SessionId == sessionId)
                .OrderBy(t => t.Order)
                .ToListAsync();

            return tasks.Select(t => ToResponse(t)).ToList();
        }

        public async Task<FocusTaskResponseDto?> GetById(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task is null ? null : ToResponse(task);
        }

        public async Task<FocusTaskResponseDto> Create(FocusTaskCreateDto dto)
        {
            var task = new FocusTask
            {
                SessionId = dto.SessionId,
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Order = dto.Order,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return ToResponse(task);
        }

        public async Task<FocusTaskResponseDto?> Complete(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return null;

            task.Status = TaskStatus.Completed;
            task.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return ToResponse(task);
        }

        public async Task<FocusTaskResponseDto?> Update(Guid id, FocusTaskUpdateDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return null;

            if (task.Status == TaskStatus.Completed)
                throw new InvalidOperationException("Cannot update a completed task.");

            if (dto.Title is not null) task.Title = dto.Title;
            if (dto.Description is not null) task.Description = dto.Description;
            if (dto.Priority is not null) task.Priority = dto.Priority.Value;
            if (dto.Order is not null) task.Order = dto.Order.Value;

            await _context.SaveChangesAsync();
            return ToResponse(task);
        }


        public async Task<bool> Delete(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public static FocusTaskResponseDto ToResponse(FocusTask task) => new()
        {
            Id = task.Id,
            SessionId = task.SessionId,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Order = task.Order,
            CreatedAt = task.CreatedAt,
            CompletedAt = task.CompletedAt
        };
    }
}
