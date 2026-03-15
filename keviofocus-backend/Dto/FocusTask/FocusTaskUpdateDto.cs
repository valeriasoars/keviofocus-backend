using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;
using TaskStatus = keviofocus_backend.Enums.TaskStatus;

namespace keviofocus_backend.Dto.FocusTask
{
    public class FocusTaskUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskPriority? Priority { get; set; }
        public int? Order { get; set; }
    }
}
