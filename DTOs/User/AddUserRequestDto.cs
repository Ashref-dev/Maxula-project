namespace Maxula_project.DTOs.User
{
    public class AddUserRequestDto
    {
        public string Email { get; set; } = "not set";
        public string Password { get; set; } = "default";
        public string FirstName { get; set; } = "default";
        public string LastName { get; set; } = "default";
        public bool IsAdmin { get; set; } = false;
        public string? Title { get; set; } = "Default";
        public string? Picture { get; set; } = "https://th.bing.com/th/id/OIP.OWHqt6GY5jrr7ETvJr8ZXwAAAA?rs=1&pid=ImgDetMain";
        public DateOnly? JoinDate { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ProductTeam { get; set; }
        public string? Tags { get; set; }
    }
}
