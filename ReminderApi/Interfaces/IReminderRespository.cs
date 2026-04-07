using ReminderApi.Models;

namespace ReminderApi.Interfaces;

public interface IReminderRespository
{
    void Add(Reminder reminder);
    IEnumerable<Reminder> GetAll();
    IEnumerable<Reminder> GetPending();
    void Update(Reminder reminder);
}
