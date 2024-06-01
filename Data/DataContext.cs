using Microsoft.EntityFrameworkCore;

namespace Maxula_project.Data;

public class DataContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>()
          .HasKey(a => a.ActivityId);

        modelBuilder.Entity<Activity>()
            .HasOne(a => a.User)
            .WithMany(u => u.Activities)
            .HasForeignKey(a => a.UserId)
            .IsRequired();
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();

    public DbSet<QrCodeModel> QRCodes => Set<QrCodeModel>();

    public DbSet<Activity> Activities { get; set; }
}
