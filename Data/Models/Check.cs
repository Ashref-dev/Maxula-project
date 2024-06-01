namespace Maxula_project.Models
{
public class Check
{
    public int ActivityId { get; set; }
    public DateTime Date { get; set; }
    public DateTime Checkin { get; set; }
    public DateTime Checkout { get; set; }
    public string Sector { get; set; }
    public int DeskID { get; set; }
    public int UserID { get; set; }
}
}
