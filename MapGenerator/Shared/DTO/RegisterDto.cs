using System.ComponentModel.DataAnnotations;

namespace Shared.DTO;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    [MaxLength(40, ErrorMessage = "Za długa wartość")]
    public string Email { get; set; }
    [Required]
    [MaxLength(40, ErrorMessage = "Za długa wartość")]
    public string Username { get; set; }
    [Required]
    [MaxLength(40, ErrorMessage = "Za długa wartość")]
    public string Password { get; set; }
    [Required]
    [Compare("Password",ErrorMessage = "Hasła nie są takie same")]
    [MaxLength(40, ErrorMessage = "Za długa wartość")]
    public string ConfirmPassword { get; set; }
}