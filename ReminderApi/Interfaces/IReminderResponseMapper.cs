using ReminderApi.Models;
using ReminderApi.Models.DTOs;

namespace ReminderApi.Interfaces;

public interface IReminderResponseMapper
{
    ReminderResponse MapToResponse(Reminder reminder);
    IEnumerable<ReminderResponse> MapToResponses(IEnumerable<Reminder> reminders);
}
