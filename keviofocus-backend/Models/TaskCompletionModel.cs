namespace keviofocus_backend.Models
{
    public class TaskCompletionModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TaskId { get; set; } = string.Empty;
        public string SessionRunId { get; set; } = string.Empty;
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

  
        public TaskItemModel Task { get; set; } = null!;
        public SessionRunModel SessionRun { get; set; } = null!;
    }
}
