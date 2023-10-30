namespace MagicVilla.Models.DTOs;

public class ErrorDto
{
    public string? Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string? TraceId { get; set; }
    public Dictionary<string, List<string>>? Errors { get; set; }
}