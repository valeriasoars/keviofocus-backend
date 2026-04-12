namespace keviofocus_backend.DTOs
{
    public record TaskCreateDto(
        string SessionId,
        string Title,
        int OrderIndex
    );
}
