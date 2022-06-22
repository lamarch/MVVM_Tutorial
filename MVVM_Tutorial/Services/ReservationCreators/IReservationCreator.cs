namespace MVVM_Tutorial.Services.ReservationCreators
{
    using MVVM_Tutorial.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IReservationCreator
    {
        Task CreateReservation(Reservation reservation);
    }
}
