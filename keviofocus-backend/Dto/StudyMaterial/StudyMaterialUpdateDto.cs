using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;

namespace keviofocus_backend.Dto.StudyMaterial
{
    public class StudyMaterialUpdateDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Note { get; set; }
    }
}
