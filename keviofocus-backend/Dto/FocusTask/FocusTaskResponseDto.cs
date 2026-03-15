using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;
using TaskStatus = keviofocus_backend.Enums.TaskStatus;

namespace keviofocus_backend.Dto.FocusTask
{
    public class FocusTaskResponseDto
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
