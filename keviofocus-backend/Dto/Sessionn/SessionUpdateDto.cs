namespace keviofocus_backend.Dto.Session
{
    public class SessionUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? FocusDurationMin { get; set; }
        public int? ShortBreakDurationMin { get; set; }
        public int? LongBreakDurationMin { get; set; }
        public int? CyclesBeforeLongBreak { get; set; }
        public int? TotalCycles { get; set; }
    }
}
