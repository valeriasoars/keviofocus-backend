using keviofocus_backend.DTOs;
using keviofocus_backend.Models;

namespace keviofocus_backend.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskItemModel> CreateTaskAsync(TaskCreateDto dto);
        Task<IEnumerable<TaskItemModel>> GetTasksBySessionIdAsync(string sessionId);
        Task<bool> DeleteTaskAsync(string id);
        Task<TaskItemModel?> ToggleTaskStatusAsync(string id);
    }
}
