
namespace Maxula_project.DTOs.User;

public class GetUserResponseDto
{
    public int UserId { get; set; }
    public string Email { get; set; } = "not set";
    public string Password { get; set; } = "default";

    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }

    public string FirstName { get; set; } = "default";
    public string LastName { get; set; } = "default";
    public bool IsAdmin { get; set; } = false;
    public string? Title { get; set; } = "Default";
    public string? Picture { get; set; } = "xsgames.co/randomusers/avatar.php?g=male";
    public DateOnly? JoinDate { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    public string? ProductTeam { get; set; }
    public string? Tags { get; set; }
}
