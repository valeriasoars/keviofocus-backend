using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;

namespace keviofocus_backend.Dto.StudyMaterial
{
    public class StudyMaterialResponseDto
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public MaterialType Type { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
