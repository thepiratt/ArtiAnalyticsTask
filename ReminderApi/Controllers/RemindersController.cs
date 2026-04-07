using Microsoft.AspNetCore.Mvc;
using ReminderApi.Interfaces;
using ReminderApi.Models.DTOs;

namespace ReminderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemindersController(IReminderService service, ILogger<RemindersController> logger) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpPost(Name = "CreateReminder")]
        public IActionResult Create(ReminderRequest request)
        {
            var reminder = service.Create(request.Message, request.SendAt, request.Email);

            return Ok(new
            {
                id = reminder.Id,
                status = reminder.Status,
                sendAt = reminder.SendAt
            });
        }

        [HttpGet(Name = "GetAllReminders")]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }
    }
}
