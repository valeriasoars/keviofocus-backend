using keviofocus_backend.Data;
using keviofocus_backend.DTOs;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Services
{
    public class TaskService : ITaskService
    {
        private readonly KevioDbContext _context;

        public TaskService(KevioDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItemModel> CreateTaskAsync(TaskCreateDto dto)
        {
            var task = new TaskItemModel
            {
                SessionId = dto.SessionId,
                Title = dto.Title,
                OrderIndex = dto.OrderIndex,
                Completed = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<IEnumerable<TaskItemModel>> GetTasksBySessionIdAsync(string sessionId)
        {
            return await _context.Tasks
               .Where(t => t.SessionId == sessionId)
               .OrderBy(t => t.OrderIndex)
               .ToListAsync<TaskItemModel>();
        }
 
        public async Task<TaskItemModel?> ToggleTaskStatusAsync(string id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            task.Completed = !task.Completed;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
