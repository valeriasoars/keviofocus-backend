using keviofocus_backend.DTOs;
using keviofocus_backend.Models;

namespace keviofocus_backend.Services.Interfaces
{
    public interface ITaskCompletionService
    {
        Task<TaskCompletionModel> CompleteTaskAsync(TaskCompletionDto dto);
        Task<bool> UndoTaskCompletionAsync(string id);
        Task<IEnumerable<TaskCompletionModel>> GetCompletionsByRunIdAsync(string runId);
    }
}
