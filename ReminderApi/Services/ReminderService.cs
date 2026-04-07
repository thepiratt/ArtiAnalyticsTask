using ReminderApi.Interfaces;
using ReminderApi.Models;

namespace ReminderApi.Services;

public class ReminderService(IReminderRespository repository) : IReminderService
{
    public Reminder Create(string message, DateTime sendAt, string? email)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message is required");

        if (sendAt <= DateTime.UtcNow)
            throw new ArgumentException("SendAt must be in the future");

        var reminder = new Reminder
        {
            Id = Guid.NewGuid(),
            Message = message,
            SendAt = sendAt,
            Email = email,
            Status = ReminderStatus.Scheduled
        };

        repository.Add(reminder);
        return reminder;
    }

    public IEnumerable<Reminder> GetAll() => repository.GetAll();
}
