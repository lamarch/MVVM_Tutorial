namespace MVVM_Tutorial.ViewModels
{
    using MVVM_Tutorial.Models;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ReservationViewModel : ViewModelBase
    {
        private readonly Reservation reservation;

        public ReservationViewModel(Reservation reservation)
        {
            this.reservation = reservation;
        }

        public string RoomID => reservation.RoomID.ToString();
        public string Username => reservation.Username;
        public string StartTime => reservation.StartTime.ToString("d");
        public string EndTime => reservation.EndTime.ToString("d");
    }
}
