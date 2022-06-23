namespace MVVM_Tutorial.ViewModels
{
    using MVVM_Tutorial.Commands;
    using MVVM_Tutorial.Services;
    using MVVM_Tutorial.Stores;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    internal class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        string username = "";
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));

                ClearErrors(nameof(Username));

                if (string.IsNullOrWhiteSpace(Username))
                {
                    AddError("The username cannot be empty.", nameof(Username));
                }

                OnPropertyChanged(nameof(CanMakeReservation));
            }
        }

        int roomNumber;
        public int RoomNumber
        {
            get { return roomNumber; }
            set
            {
                roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));

                ClearErrors(nameof(RoomNumber));

                if (FloorNumber <= 0)
                {
                    AddError("The room number must be greater than 0.", nameof(RoomNumber));
                }

                OnPropertyChanged(nameof(CanMakeReservation));
            }
        }

        int floorNumber;
        public int FloorNumber
        {
            get { return floorNumber; }
            set
            {
                floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));

                ClearErrors(nameof(FloorNumber));

                if (FloorNumber <= 0)
                {
                    AddError("The floor number must be greater than 0.", nameof(FloorNumber));
                }

                OnPropertyChanged(nameof(CanMakeReservation));
            }
        }

        DateTime startTime = new DateTime(2022, 1, 1);
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                OnPropertyChanged(nameof(StartTime));

                ClearErrors(nameof(StartTime));
                ClearErrors(nameof(EndTime));

                if (EndTime < StartTime)
                {
                    AddError("The start time cannot be after the end time.", nameof(StartTime));
                }

                OnPropertyChanged(nameof(CanMakeReservation));
            }
        }

        DateTime endTime = new DateTime(2022, 1, 2);
        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                OnPropertyChanged(nameof(EndTime));

                ClearErrors(nameof(StartTime));
                ClearErrors(nameof(EndTime));

                if (EndTime < StartTime)
                {
                    AddError("The end time cannot be before the start time.", nameof(EndTime));
                }

                OnPropertyChanged(nameof(CanMakeReservation));
            }
        }

        public bool CanMakeReservation =>
            !string.IsNullOrWhiteSpace(Username) &&
            FloorNumber > 0 &&
            RoomNumber > 0 &&
            StartTime < EndTime;

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }


        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationListingViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationListingViewNavigationService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationListingViewNavigationService);
        }

    }
}
