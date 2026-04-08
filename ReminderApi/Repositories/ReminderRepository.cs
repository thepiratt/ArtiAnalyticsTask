using ReminderApi.Interfaces;
using ReminderApi.Models;
using System.Collections.Concurrent;

namespace ReminderApi.Repositories;

public class ReminderRepository : IReminderRespository
{
    private readonly ConcurrentDictionary<Guid, Reminder> _reminders = new();

    public void Add(Reminder reminder)
        => _reminders[reminder.Id] = reminder;

    public IEnumerable<Reminder> GetAll()
        => _reminders.Values;

    public IEnumerable<Reminder> GetPending()
        => _reminders.Values.Where(r => r.Status == ReminderStatus.Scheduled);

    public void Update(Reminder reminder)
        => _reminders[reminder.Id] = reminder;
    public Reminder? GetById(Guid id)
        => _reminders.TryGetValue(id, out var reminder) ? reminder : null;
}
