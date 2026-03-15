using keviofocus_backend.Dto.Cycle;
using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;

namespace keviofocus_backend.Interfaces
{
    public interface ICycleService
    {
        Task<List<CycleResponseDto>> GetAllBySession(Guid sessionId);
        Task<CycleResponseDto?> GetById(Guid id);
        Task<CycleResponseDto?> UpdateStatus(Guid id, CycleStatus status);


    }
}
