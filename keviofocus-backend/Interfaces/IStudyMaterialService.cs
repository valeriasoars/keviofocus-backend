using keviofocus_backend.Dto.StudyMaterial;

namespace keviofocus_backend.Interfaces
{
    public interface IStudyMaterialService
    {
        Task<List<StudyMaterialResponseDto>> GetAllBySession(Guid sessionId);
        Task<StudyMaterialResponseDto?> GetById(Guid id);
        Task<StudyMaterialResponseDto> Create(StudyMaterialCreateDto dto);
        Task<StudyMaterialResponseDto?> Update(Guid id, StudyMaterialUpdateDto dto);
        Task<bool> Delete(Guid id);
    }
}
