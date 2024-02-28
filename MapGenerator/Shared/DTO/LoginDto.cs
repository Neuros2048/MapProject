using System.ComponentModel.DataAnnotations;

namespace Shared.DTO;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [MaxLength(40, ErrorMessage = "Za długa wartość")]
    public string Email { get; set; }
    [Required]
    [MaxLength(40, ErrorMessage = "Za długa wartość")]
    public string Password { get; set; }
}