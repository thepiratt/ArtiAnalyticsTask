using ReminderApi.Interfaces;
using ReminderApi.Models;

namespace ReminderApi.BackgroundServices;

public class ReminderWorker(IReminderRespository repository, IEmailService emailService, ILogger<ReminderWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;

            var dueReminders = repository
                .GetPending()
                .Where(r => r.SendAt <= now)
                .ToList();

            foreach (var reminder in dueReminders)
            {
                try
                {
                    await emailService.SendEmailAsync(reminder.Email, "Your Reminder", reminder.Message);
                    logger.LogInformation("[{Time}] Reminder sent: {Message}, to {email}.", now, reminder.Message, reminder.Email);

                    reminder.Status = ReminderStatus.Sent;
                    repository.Update(reminder);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to send reminder {ReminderId}", reminder.Id);
                    reminder.Status = ReminderStatus.Failed;
                    repository.Update(reminder);
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
