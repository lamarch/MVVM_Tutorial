namespace MVVM_Tutorial.Services.ReservationCreators;

using MVVM_Tutorial.Contexts;
using MVVM_Tutorial.Converters;
using MVVM_Tutorial.Models;

using System.Threading.Tasks;

internal class DatabaseReservationCreator : IReservationCreator
{
    private readonly ReservoomDbContextFactory dbContextFactory;

    public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task CreateReservation(Reservation reservation)
    {
        using ReservoomDbContext context = dbContextFactory.CreateDbContext();

        context.Reservations.Add(reservation.ToReservationDTO());

        await context.SaveChangesAsync();
    }
}
