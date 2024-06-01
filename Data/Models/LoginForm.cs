using System.ComponentModel.DataAnnotations;
namespace Maxula_project.Models;
public class LoginForm
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
