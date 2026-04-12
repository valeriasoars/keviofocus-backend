using keviofocus_backend.DTOs;
using keviofocus_backend.Emuns;
using keviofocus_backend.Models;

namespace keviofocus_backend.Services.Interfaces
{
    public interface ISessionRunService
    {
        Task<SessionRunModel> StartRunAsync(SessionRunCreateDto dto);
        Task<SessionRunModel?> UpdateStatusAsync(string runId, StatusEnum status, int focusSeconds, int breakSeconds);
        Task<SessionRunModel?> FinishRunAsync(string runId);
        Task<IEnumerable<SessionRunModel>> GetHistoryBySessionIdAsync(string sessionId);
    }
}
