using keviofocus_backend.Dto.Session;

namespace keviofocus_backend.Interfaces
{
    public interface ISessionService
    {
        Task<List<SessionResponseDto>> GetAll();
        Task<SessionResponseDto?> GetById(Guid id);
        Task<SessionResponseDto> Create(SessionCreateDto dto);
        Task<SessionResponseDto?> Update(Guid id, SessionUpdateDto dto);
        Task<bool> Delete(Guid id);

        Task<SessionResponseDto?> Start(Guid id);
        Task<SessionResponseDto?> Pause(Guid id);
        Task<SessionResponseDto?> Resume(Guid id);
        Task<SessionResponseDto?> Finish(Guid id);
    }
}
