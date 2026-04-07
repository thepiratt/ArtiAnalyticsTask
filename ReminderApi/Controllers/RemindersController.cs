using Microsoft.AspNetCore.Mvc;
using ReminderApi.Interfaces;
using ReminderApi.Models.DTOs;

namespace ReminderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemindersController(IReminderService service, IReminderResponseMapper mapper, ILogger<RemindersController> logger) : ControllerBase
    {

        [HttpPost(Name = "CreateReminder")]
        public IActionResult Create(ReminderRequest request)
        {
            var reminder = service.Create(request.Message, request.SendAt, request.Email);

            return Ok(mapper.MapToResponse(reminder));
        }

        [HttpGet(Name = "GetAllReminders")]
        public IActionResult GetAll()
        {
            return Ok(mapper.MapToResponses(service.GetAll()));
        }
    }
}
