namespace MVVM_Tutorial.ViewModels
{
    using MVVM_Tutorial.Models;
    using MVVM_Tutorial.Stores;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        public ViewModelBase? CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;

            navigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
        }

        private void CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
