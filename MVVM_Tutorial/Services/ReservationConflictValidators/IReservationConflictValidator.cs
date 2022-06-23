namespace MVVM_Tutorial.Services.ReservationConflictValidators;

using MVVM_Tutorial.Models;

using System.Threading.Tasks;

internal interface IReservationConflictValidator
{
    Task<Reservation?> GetConflictingReservation(Reservation reservation);
}
