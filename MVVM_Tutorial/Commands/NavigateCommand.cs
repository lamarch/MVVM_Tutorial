namespace MVVM_Tutorial.Commands
{
    using MVVM_Tutorial.Models;
    using MVVM_Tutorial.Services;
    using MVVM_Tutorial.Stores;
    using MVVM_Tutorial.ViewModels;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
}
