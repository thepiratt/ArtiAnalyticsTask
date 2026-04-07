using ReminderApi.Interfaces;
using ReminderApi.Models;

namespace ReminderApi.BackgroundServices;

public class ReminderWorker(IReminderRespository repository, ILogger<ReminderWorker> logger) : BackgroundService
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
                logger.LogInformation("[{Time}] Reminder sent: {Message}", now, reminder.Message);

                reminder.Status = ReminderStatus.Sent;
                repository.Update(reminder);
            }

            await Task.Delay(5000, stoppingToken);
        }
    }
}
