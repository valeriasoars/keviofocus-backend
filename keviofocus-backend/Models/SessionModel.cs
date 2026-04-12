namespace keviofocus_backend.Models
{
    public class SessionModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int FocusDurationMinutes { get; set; }
        public int BreakDurationMinutes { get; set; }
        public int Cycles { get; set; }
        public string? Color { get; set; }
        public string? Icon { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskItemModel> Tasks { get; set; } = new List<TaskItemModel>();
        public ICollection<SessionRunModel> Runs { get; set; } = new List<SessionRunModel>();
    }
}
