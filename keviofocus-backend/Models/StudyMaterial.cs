using keviofocus_backend.Enums;

namespace keviofocus_backend.Models
{
    public class StudyMaterial
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public MaterialType Type { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
