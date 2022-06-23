namespace MVVM_Tutorial.Services.ReservationProviders;

using Microsoft.EntityFrameworkCore;

using MVVM_Tutorial.Contexts;
using MVVM_Tutorial.Converters;
using MVVM_Tutorial.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class DatabaseReservationProvider : IReservationProvider
{
    private readonly ReservoomDbContextFactory dbContextFactory;

    public DatabaseReservationProvider(ReservoomDbContextFactory dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        using ReservoomDbContext context = dbContextFactory.CreateDbContext();
        var reservationDTOs = await context.Reservations.ToListAsync();

        await Task.Delay(1000);

        return reservationDTOs.Select(dto => dto.ToReservationModel());
    }
}
