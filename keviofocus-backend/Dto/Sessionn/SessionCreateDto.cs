namespace keviofocus_backend.Dto.Session
{
    public class SessionCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int FocusDurationMin { get; set; } = 25;
        public int ShortBreakDurationMin { get; set; } = 5;
        public int LongBreakDurationMin { get; set; } = 15;
        public int CyclesBeforeLongBreak { get; set; } = 4;
        public int TotalCycles { get; set; } = 4;
    }
}
