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
        public IActionResult Create([FromBody] ReminderRequest request)
        {
            var reminder = service.Create(request.Message, request.SendAt, request.Email);

            var response = mapper.MapToResponse(reminder);
            return CreatedAtRoute("GetReminderById", new { id = reminder.Id }, response);
        }

        [HttpGet(Name = "GetAllReminders")]
        public IActionResult GetAll()
        {
            return Ok(mapper.MapToResponses(service.GetAll()));
        }

        [HttpGet("{id:guid}", Name = "GetReminderById")]
        public IActionResult Get(Guid id)
        {
            var reminder = service.GetById(id);
            if (reminder is null)
                return NotFound();

            return Ok(mapper.MapToResponse(reminder));
        }
    }
}
