using System.ComponentModel.DataAnnotations;
namespace Maxula_project.Models;
public class RegisterForm
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
    [MaxLength(32, ErrorMessage = "Password at most 32 characters long.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,32}$", ErrorMessage = "Password must contain at least one character and one number.")]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Firstname is required.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Lastname is required.")]
    public string? LastName { get; set; }

    [Required]
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
    public bool AcceptTerms { get; set; }
}
