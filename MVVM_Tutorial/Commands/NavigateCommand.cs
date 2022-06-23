namespace MVVM_Tutorial.Commands;

using MVVM_Tutorial.Services;
using MVVM_Tutorial.ViewModels;

internal class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
{
    private readonly NavigationService<TViewModel> navigationService;

    public NavigateCommand(NavigationService<TViewModel> navigationService)
    {
        this.navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        navigationService.Navigate();
    }
}
