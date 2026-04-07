namespace ReminderApi.Models.DTOs;

public class ReminderResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SendAt { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; } = string.Empty;
}
