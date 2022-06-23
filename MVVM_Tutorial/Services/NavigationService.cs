namespace MVVM_Tutorial.Services;

using MVVM_Tutorial.Stores;
using MVVM_Tutorial.ViewModels;

using System;

internal class NavigationService<TViewModel> where TViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly Func<ViewModelBase> createViewModel;

    public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
    {
        this.navigationStore = navigationStore;
        this.createViewModel = createViewModel;
    }

    public void Navigate()
    {
        navigationStore.CurrentViewModel = createViewModel();
    }
}
