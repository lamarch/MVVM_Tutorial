namespace MVVM_Tutorial.Commands;

using MVVM_Tutorial.Stores;
using MVVM_Tutorial.ViewModels;

using System;
using System.Threading.Tasks;

internal class LoadReservationCommand : AsyncCommandBase
{
    private readonly HotelStore hotelStore;
    private readonly ReservationListingViewModel viewModel;

    public LoadReservationCommand(HotelStore hotelStore, ReservationListingViewModel viewModel)
    {
        this.hotelStore = hotelStore;
        this.viewModel = viewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        viewModel.ErrorMessage = null;
        viewModel.IsLoading = true;
        try
        {
            await hotelStore.Load();

            viewModel.UpdateReservations(hotelStore.Reservations);
        }
        catch (Exception)
        {
            viewModel.ErrorMessage = "Failed to load reservations.";
        }
        viewModel.IsLoading = false;
    }
}
