using static System.Collections.Specialized.BitVector32;

namespace keviofocus_backend.Models
{
    public class TaskItemModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SessionId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public SessionModel Session { get; set; } = null!;
        public ICollection<TaskCompletionModel> Completions { get; set; } = new List<TaskCompletionModel>();
    }
}
