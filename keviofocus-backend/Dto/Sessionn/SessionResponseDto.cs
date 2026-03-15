using keviofocus_backend.Enums;

namespace keviofocus_backend.Dto.Session
{
    public class SessionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int FocusDurationMin { get; set; }
        public int ShortBreakDurationMin { get; set; }
        public int LongBreakDurationMin { get; set; }
        public int CyclesBeforeLongBreak { get; set; }
        public int TotalCycles { get; set; }
        public int CompletedCycles { get; set; }
        public SessionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
