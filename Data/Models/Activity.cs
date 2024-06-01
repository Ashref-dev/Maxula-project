using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maxula_project.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly CheckIn { get; set; }

        public TimeOnly? CheckOut { get; set; }

        [Required]
        [StringLength(100)]
        public string Sector { get; set; } = string.Empty;

        [Required]
        public int DeskId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        public Activity() { }

        public Activity(DateOnly date, TimeOnly checkIn, string sector, int deskId, int userId)
        {
            Date = date;
            CheckIn = checkIn;
            Sector = sector ?? throw new ArgumentNullException(nameof(sector));
            DeskId = deskId;
            UserId = userId;
        }
    }
}
