namespace MVVM_Tutorial.Stores
{
    using MVVM_Tutorial.ViewModels;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NavigationStore
    {
        private ViewModelBase? currentViewModel;

        public ViewModelBase? CurrentViewModel
        {
            get => currentViewModel; 
            set
            {
                currentViewModel?.Dispose();
                currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action? CurrentViewModelChanged;
    }
}
