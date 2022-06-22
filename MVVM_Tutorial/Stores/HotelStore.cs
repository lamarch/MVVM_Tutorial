namespace MVVM_Tutorial.Stores
{
    using MVVM_Tutorial.Models;

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class HotelStore
    {
        private readonly Hotel hotel;
        private readonly List<Reservation> reservations;

        private Lazy<Task> initializeLazy;

        public IEnumerable<Reservation> Reservations => reservations;

        public event Action<Reservation>? ReservationMade;

        public HotelStore(Hotel hotel)
        {
            this.hotel = hotel;
            initializeLazy = new Lazy<Task>(Initialize);

            reservations = new List<Reservation>();
        }

        public async Task Load()
        {
            try
            {
                await initializeLazy.Value;
            }
            catch (Exception)
            {
                initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await hotel.MakeReservation(reservation);

            reservations.Add(reservation);

            OnReservationMade(reservation);
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }

        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await hotel.GetAllReservations();

            this.reservations.Clear();
            this.reservations.AddRange(reservations);
        }
    }
}
