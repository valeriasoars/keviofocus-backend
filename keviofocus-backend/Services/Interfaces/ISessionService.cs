using keviofocus_backend.DTOs;
using keviofocus_backend.Models;

namespace keviofocus_backend.Services.Interfaces
{
    public interface ISessionService
    {
        Task<SessionModel> CreateSessionAsync(SessionCreateDto dto);
        Task<IEnumerable<SessionModel>> GetAllSessionsAsync();
        Task<SessionModel?> GetSessionByIdAsync(string id);
    }
}
