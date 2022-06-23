namespace MVVM_Tutorial.Services.ReservationCreators;

using MVVM_Tutorial.Contexts;
using MVVM_Tutorial.DTOs;
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
        ReservationDTO reservationDTO = ToReservationDTO(reservation);

        context.Reservations.Add(reservationDTO);

        await context.SaveChangesAsync();
    }

    private ReservationDTO ToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomID.FloorNumber,
            RoomNumber = reservation.RoomID.RoomNumber,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime,
            Username = reservation.Username,
        };
    }
}
