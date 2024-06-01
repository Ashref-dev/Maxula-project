using System.ComponentModel.DataAnnotations;
namespace Maxula_project.Models;

public class QrCodeModel
{
    [Key]
    public int CodeId { get; set; }

    [Required(ErrorMessage = "Sector is required.")]
    public string Sector { get; set; } = "";

    [Required(ErrorMessage = "Desk ID is required.")]
    public int DeskId { get; set; }
}
