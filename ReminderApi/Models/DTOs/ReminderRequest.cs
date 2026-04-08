using System.ComponentModel.DataAnnotations;

namespace ReminderApi.Models.DTOs;

public class ReminderRequest
{
    [Required(ErrorMessage = "Message is required")]
    [MinLength(1, ErrorMessage = "Message cannot be empty")]
    public string Message { get; set; } = string.Empty;
    [CustomValidation(typeof(ReminderRequest), nameof(ValidateSendAt))]
    public DateTime SendAt { get; set; }
    [EmailAddress(ErrorMessage = "Email must be a valid email address")]
    public string Email { get; set; } = string.Empty;

    public static ValidationResult? ValidateSendAt(DateTime sendAt, ValidationContext context)
    {
        if (sendAt <= DateTime.UtcNow)
            return new ValidationResult("SendAt must be in the future");
        return ValidationResult.Success;
    }
}
