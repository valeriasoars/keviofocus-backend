using keviofocus_backend.Enums;

namespace keviofocus_backend.Models
{
    public class Cycle
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public int Number { get; set; }
        public CycleStatus Status { get; set; } = CycleStatus.Pending;
        public DateTime? FocusStart { get; set; }
        public DateTime? FocusEnd { get; set; }
        public DateTime? BreakStart { get; set; }
        public DateTime? BreakEnd { get; set; }
        public int? FocusElapsedSeconds { get; set; }
    }
}
