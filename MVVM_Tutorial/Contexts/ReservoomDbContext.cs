
namespace MVVM_Tutorial.Contexts;

using Microsoft.EntityFrameworkCore;

using MVVM_Tutorial.DTOs;

internal class ReservoomDbContext : DbContext
{
    public ReservoomDbContext(DbContextOptions options) : base(options)
    {
    }

#nullable disable
    public DbSet<ReservationDTO> Reservations { get; set; }
#nullable restore
}
