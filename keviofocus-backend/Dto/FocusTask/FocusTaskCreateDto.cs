using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;
using TaskStatus = keviofocus_backend.Enums.TaskStatus;

namespace keviofocus_backend.Dto.FocusTask
{
    public class FocusTaskCreateDto
    {
        public Guid SessionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public int Order { get; set; }
    }
}
