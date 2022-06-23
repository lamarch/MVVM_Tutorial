namespace MVVM_Tutorial.Services.ReservationConflictValidators;

using Microsoft.EntityFrameworkCore;

using MVVM_Tutorial.Contexts;
using MVVM_Tutorial.Converters;
using MVVM_Tutorial.Models;

using System.Linq;
using System.Threading.Tasks;

internal class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly ReservoomDbContextFactory dbContextFactory;

    public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<Reservation?> GetConflictingReservation(Reservation reservation)
    {
        using ReservoomDbContext context = dbContextFactory.CreateDbContext();
        var dto = await context.Reservations
            .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
            .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
            .Where(r => r.StartTime < reservation.EndTime)
            .Where(r => r.EndTime > reservation.StartTime)
            .FirstOrDefaultAsync();

        return dto is null ? null : dto.ToReservationModel();
    }
}
