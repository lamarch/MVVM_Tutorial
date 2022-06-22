namespace MVVM_Tutorial.Commands
{
    using MVVM_Tutorial.Exceptions;
    using MVVM_Tutorial.Models;
    using MVVM_Tutorial.Services;
    using MVVM_Tutorial.Stores;
    using MVVM_Tutorial.ViewModels;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    internal class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel makeReservationViewModel;
        private readonly HotelStore hotelStore;
        private readonly NavigationService<ReservationListingViewModel> reservationViewNavigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel,
                                      HotelStore hotelStore,
                                      NavigationService<ReservationListingViewModel> reservationListingViewNavigationService)
        {
            this.makeReservationViewModel = makeReservationViewModel;
            this.hotelStore = hotelStore;
            this.reservationViewNavigationService = reservationListingViewNavigationService;
            makeReservationViewModel.PropertyChanged += OnPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                   && !string.IsNullOrWhiteSpace(makeReservationViewModel.Username)
                   && makeReservationViewModel.FloorNumber > 0
                   && makeReservationViewModel.RoomNumber > 0;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            var reservation = new Reservation(
                new RoomID(makeReservationViewModel.FloorNumber, makeReservationViewModel.RoomNumber),
                makeReservationViewModel.StartTime, 
                makeReservationViewModel.EndTime, 
                makeReservationViewModel.Username);
            try
            {
                await hotelStore.MakeReservation(reservation);

                MessageBox.Show("Successfully reserved room.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                reservationViewNavigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken at this time.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to make reservation.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MakeReservationViewModel.Username)
                || e.PropertyName == nameof(MakeReservationViewModel.FloorNumber)
                || e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
                base.OnCanExecuteChanged();
        }
    }
}
