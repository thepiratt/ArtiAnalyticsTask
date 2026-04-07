using ReminderApi.Interfaces;
using ReminderApi.Models;

namespace ReminderApi.Services;

public class ReminderService(IReminderRespository repository, IReminderValidator validator) : IReminderService
{
    public Reminder Create(string message, DateTime sendAt, string? email)
    {
        validator.ValidateCreateRequest(message, sendAt);

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
