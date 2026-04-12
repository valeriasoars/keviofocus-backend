namespace keviofocus_backend.DTOs
{
    public record SessionCreateDto(
        string Name,
        string? Description,
        int FocusDurationMinutes,
        int BreakDurationMinutes,
        int Cycles,
        string? Color,
        string? Icon
    );
}
