using keviofocus_backend.Data;
using keviofocus_backend.DTOs;
using keviofocus_backend.Models;
using keviofocus_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Services
{
    public class TaskCompletionService: ITaskCompletionService
    {
        private readonly KevioDbContext _context;

        public TaskCompletionService(KevioDbContext context) => _context = context;
        public async Task<TaskCompletionModel> CompleteTaskAsync(TaskCompletionDto dto)
        {
            var completion = new TaskCompletionModel
            {
                TaskId = dto.TaskId,
                SessionRunId = dto.SessionRunId,
                CompletedAt = DateTime.UtcNow
            };

            _context.TaskCompletions.Add(completion);

       
            var task = await _context.Tasks.FindAsync(dto.TaskId);
            if (task != null) task.Completed = true;

            await _context.SaveChangesAsync();
            return completion;
        }

        public async Task<bool> UndoTaskCompletionAsync(string id)
        {
            var completion = await _context.TaskCompletions.FindAsync(id);
            if (completion == null) return false;

            _context.TaskCompletions.Remove(completion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskCompletionModel>> GetCompletionsByRunIdAsync(string runId)
        {
            return await _context.TaskCompletions
                .Where(tc => tc.SessionRunId == runId)
                .Include(tc => tc.Task)
                .ToListAsync();
        }


    }
}
