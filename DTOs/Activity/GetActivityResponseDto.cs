namespace Maxula_project.DTOs.Activity;

public class GetActivityResponseDto
{
    public int ActivityId { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly CheckIn { get; set; }
    public TimeOnly? CheckOut { get; set; }
    public required string Sector { get; set; }
    public required int DeskId { get; set; }

    // Foreign key
    public required int UserId { get; set; }
    // Navigation property
    public required GetUserResponseDto User { get; set; }
}
