using keviofocus_backend.Dto.FocusTask;

namespace keviofocus_backend.Interfaces
{
    public interface IFocusTaskService
    {
        Task<List<FocusTaskResponseDto>> GetAllBySession(Guid sessionId);
        Task<FocusTaskResponseDto?> GetById(Guid id);
        Task<FocusTaskResponseDto> Create(FocusTaskCreateDto dto);
        Task<FocusTaskResponseDto?> Update(Guid id, FocusTaskUpdateDto dto);
        Task<FocusTaskResponseDto?> Complete(Guid id);
        Task<bool> Delete(Guid id);
    }
}
