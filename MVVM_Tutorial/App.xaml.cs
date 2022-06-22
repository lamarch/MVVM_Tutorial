namespace MVVM_Tutorial
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using MVVM_Tutorial.Contexts;
    using MVVM_Tutorial.Extensions;
    using MVVM_Tutorial.Models;
    using MVVM_Tutorial.Services;
    using MVVM_Tutorial.Services.ReservationConflictValidators;
    using MVVM_Tutorial.Services.ReservationCreators;
    using MVVM_Tutorial.Services.ReservationProviders;
    using MVVM_Tutorial.Stores;
    using MVVM_Tutorial.ViewModels;

    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost host;

        public App()
        {
            host = Host
                .CreateDefaultBuilder()
                .AddMainWindow()
                .AddModels()
                .AddViewModels()
                .AddStores()
                .AddServices()
                .AddDb()
                .Build();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            var dbContextFactory = host.Services.GetRequiredService<ReservoomDbContextFactory>();

            using (ReservoomDbContext context = dbContextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }

            var navigationService = host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.Dispose();

            base.OnExit(e);
        }
    }
}
