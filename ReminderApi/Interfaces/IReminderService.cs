using ReminderApi.Models;

namespace ReminderApi.Interfaces;

public interface IReminderService
{
    Reminder Create(string message, DateTime sendAt, string? email);
    IEnumerable<Reminder> GetAll();
    Reminder? GetById(Guid id);
}
