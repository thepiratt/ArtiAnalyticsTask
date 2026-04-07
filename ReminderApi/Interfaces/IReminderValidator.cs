namespace ReminderApi.Interfaces;

public interface IReminderValidator
{
    void ValidateCreateRequest(string message, DateTime sendAt);
}
