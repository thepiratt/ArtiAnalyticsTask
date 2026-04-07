namespace ReminderApi.Models;

public class Reminder
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SendAt { get; set; }
    public string? Email { get; set; }
    public ReminderStatus Status { get; set; }
}
