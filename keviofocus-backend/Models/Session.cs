using keviofocus_backend.Enums;

namespace keviofocus_backend.Models
{
    public class Session
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int FocusDurationMin { get; set; } = 25;
        public int ShortBreakDurationMin { get; set; } = 5;
        public int LongBreakDurationMin { get; set; } = 15;
        public int CyclesBeforeLongBreak { get; set; } = 4;
        public int TotalCycles { get; set; } = 4;
        public SessionStatus Status { get; set; } = SessionStatus.Pending;
        public int CompletedCycles { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }


        public ICollection<Cycle> Cycles { get; set; } = [];
        public ICollection<FocusTask> Tasks { get; set; } = [];
        public ICollection<StudyMaterial> Materials { get; set; } = [];

    }
}
