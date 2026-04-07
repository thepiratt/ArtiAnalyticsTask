namespace ReminderApi.Models.DTOs;

public class ReminderRequest
{
    public string Message { get; set; } = string.Empty;
    public DateTime SendAt { get; set; }
    public string? Email { get; set; }
}
