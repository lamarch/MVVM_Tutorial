namespace MVVM_Tutorial.Services.ReservationProviders;

using MVVM_Tutorial.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

internal interface IReservationProvider
{
    Task<IEnumerable<Reservation>> GetAllReservations();
}
