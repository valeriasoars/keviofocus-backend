using keviofocus_backend.Emuns;
using static System.Collections.Specialized.BitVector32;

namespace keviofocus_backend.Models
{
    public class SessionRunModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SessionId { get; set; } = string.Empty;
        public StatusEnum Status { get; set; } = StatusEnum.running;
        public int CurrentCycle { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FinishedAt { get; set; }
        public int TotalFocusSeconds { get; set; }
        public int TotalBreakSeconds { get; set; }

        public SessionModel Session { get; set; } = null!;
        public ICollection<TaskCompletionModel> TaskCompletions { get; set; } = new List<TaskCompletionModel>();
    }
}
