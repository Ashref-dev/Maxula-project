namespace Maxula_project.DTOs.Activity
{
    public class AddActivityRequestDto
    {

        public required DateOnly Date { get; set; }
        public required TimeOnly CheckIn { get; set; }
        public TimeOnly CheckOut { get; set; }
        public required string? Sector { get; set; }
        public required int DeskId { get; set; }

        // Foreign key
        public required int UserId { get; set; }
        // Navigation property
        public required Models.User User { get; set; }
    }
}
