namespace ReminderApi.Services;

public class ReminderValidator
{
    public void ValidateCreateRequest(string message, DateTime sendAt)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message is required");

        if (sendAt <= DateTime.UtcNow)
            throw new ArgumentException("SendAt must be in the future");
    }
}
