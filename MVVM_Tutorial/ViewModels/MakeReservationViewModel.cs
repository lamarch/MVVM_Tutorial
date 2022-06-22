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
            set { username = value; OnPropertyChanged(nameof(Username)); }
        }

        int roomNumber;
        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
        }

        int floorNumber;
        public int FloorNumber
        {
            get { return floorNumber; }
            set { floorNumber = value; OnPropertyChanged(nameof(FloorNumber)); }
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
            }
        }


        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }


        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationListingViewModel> reservationListingViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationListingViewNavigationService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationListingViewNavigationService);

            propertyNameToErrorsDictionnary = new();
        }

        // Error handling region

        private readonly Dictionary<string, List<string>> propertyNameToErrorsDictionnary;

        public bool HasErrors => propertyNameToErrorsDictionnary.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        private void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyNameToErrorsDictionnary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void AddError(string errorMessage, string propertyName)
        {
            if (!propertyNameToErrorsDictionnary.ContainsKey(propertyName))
            {
                propertyNameToErrorsDictionnary[propertyName] = new List<string>();
            }
            propertyNameToErrorsDictionnary[propertyName].Add(errorMessage);

            OnErrorsChanged(new(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            propertyNameToErrorsDictionnary.Remove(propertyName);

            OnErrorsChanged(new(propertyName));
        }
    }
}
