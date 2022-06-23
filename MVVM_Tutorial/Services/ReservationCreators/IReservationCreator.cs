namespace MVVM_Tutorial.Services.ReservationCreators;

using MVVM_Tutorial.Models;

using System.Threading.Tasks;

internal interface IReservationCreator
{
    Task CreateReservation(Reservation reservation);
}
