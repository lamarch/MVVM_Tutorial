namespace MVVM_Tutorial.ViewModels;

using MVVM_Tutorial.Commands;
using MVVM_Tutorial.Models;
using MVVM_Tutorial.Services;
using MVVM_Tutorial.Stores;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

internal class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> reservations;
    private readonly HotelStore hotelStore;

    public IEnumerable<ReservationViewModel> Reservations => reservations;
    public bool HasReservations => Reservations.Any();

    private string? errorMessage;
    public string? ErrorMessage
    {
        get => errorMessage;
        set
        {
            errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
            OnPropertyChanged(nameof(HasErrorMessage));
        }
    }

    public bool HasErrorMessage => !string.IsNullOrWhiteSpace(ErrorMessage);

    private bool isLoading;
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public ICommand MakeReservationCommand { get; }
    public ICommand LoadReservationCommand { get; }

    public ReservationListingViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> makeReservationNavigationService)
    {
        this.hotelStore = hotelStore;

        reservations = new();

        MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(makeReservationNavigationService);
        LoadReservationCommand = new LoadReservationCommand(hotelStore, this);

        hotelStore.ReservationMade += OnReservationMade;
        reservations.CollectionChanged += OnReservationsChanged;
    }

    public override void Dispose()
    {
        hotelStore.ReservationMade -= OnReservationMade;
        base.Dispose();
    }

    private void OnReservationsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(HasReservations));
    }

    private void OnReservationMade(Reservation reservation)
    {
        AddReservation(reservation);
    }

    private void AddReservation(Reservation reservation)
    {
        ReservationViewModel reservationViewModel = new(reservation);
        this.reservations.Add(reservationViewModel);
    }

    public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new(hotelStore, makeReservationNavigationService);

        viewModel.LoadReservationCommand.Execute(null);

        return viewModel;
    }

    public void UpdateReservations(IEnumerable<Reservation> reservations)
    {
        this.reservations.Clear();

        foreach (var reservation in reservations)
        {
            AddReservation(reservation);
        }
    }
}
