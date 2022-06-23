namespace MVVM_Tutorial.Models;

using MVVM_Tutorial.Exceptions;
using MVVM_Tutorial.Services.ReservationConflictValidators;
using MVVM_Tutorial.Services.ReservationCreators;
using MVVM_Tutorial.Services.ReservationProviders;

using System.Collections.Generic;
using System.Threading.Tasks;

internal class ReservationBook
{
    private readonly IReservationProvider reservationProvider;
    private readonly IReservationCreator reservationCreator;
    private readonly IReservationConflictValidator reservationConflictValidator;

    public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
    {
        this.reservationProvider = reservationProvider;
        this.reservationCreator = reservationCreator;
        this.reservationConflictValidator = reservationConflictValidator;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        return await reservationProvider.GetAllReservations();
    }

    public async Task AddReservation(Reservation reservation)
    {
        var conflictingReservation = await reservationConflictValidator.GetConflictingReservation(reservation);

        if (conflictingReservation is not null)
        {
            throw new ReservationConflictException(conflictingReservation, reservation);
        }

        await reservationCreator.CreateReservation(reservation);
    }


}
