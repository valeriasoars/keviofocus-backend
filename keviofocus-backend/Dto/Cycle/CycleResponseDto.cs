using keviofocus_backend.Dto.Session;
using keviofocus_backend.Enums;


namespace keviofocus_backend.Dto.Cycle
{
    public class CycleResponseDto
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public int Number { get; set; }
        public CycleStatus Status { get; set; }
        public DateTime? FocusStart { get; set; }
        public DateTime? FocusEnd { get; set; }
        public DateTime? BreakStart { get; set; }
        public DateTime? BreakEnd { get; set; }
        public int? FocusElapsedSeconds { get; set; }
    }
}
