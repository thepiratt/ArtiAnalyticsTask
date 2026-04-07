using ReminderApi.Interfaces;
using ReminderApi.Models;
using ReminderApi.Models.DTOs;

namespace ReminderApi.Services;

public class ReminderResponseMapper : IReminderResponseMapper
{
    public ReminderResponse MapToResponse(Reminder reminder)
    {
        return new ReminderResponse
        {
            Id = reminder.Id,
            Message = reminder.Message,
            SendAt = reminder.SendAt,
            Email = reminder.Email,
            Status = reminder.Status.ToString()
        };
    }

    public IEnumerable<ReminderResponse> MapToResponses(IEnumerable<Reminder> reminders)
    {
        return reminders.Select(MapToResponse);
    }
}
