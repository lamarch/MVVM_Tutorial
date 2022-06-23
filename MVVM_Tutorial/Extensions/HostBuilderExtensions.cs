namespace MVVM_Tutorial.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MVVM_Tutorial.Contexts;
using MVVM_Tutorial.Models;
using MVVM_Tutorial.Services;
using MVVM_Tutorial.Services.ReservationConflictValidators;
using MVVM_Tutorial.Services.ReservationCreators;
using MVVM_Tutorial.Services.ReservationProviders;
using MVVM_Tutorial.Stores;
using MVVM_Tutorial.ViewModels;

using System;

internal static class HostBuilderExtensions
{
    public static IHostBuilder AddModels(this IHostBuilder builder)
    {
        return builder.ConfigureServices((hostContext, services) =>
        {
            services.AddTransient<ReservationBook>();

            string hotelName = hostContext.Configuration.GetValue<string>("HotelName");
            services.AddSingleton<Hotel>(s => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));
        });
    }

    public static IHostBuilder AddStores(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton<HotelStore>();
            services.AddSingleton<NavigationStore>();
        });
    }

    public static IHostBuilder AddServices(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
            services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
            services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();
        });
    }

    public static IHostBuilder AddDb(this IHostBuilder builder)
    {
        return builder.ConfigureServices((hostContext, services) =>
        {
            string connectionString = hostContext.Configuration.GetConnectionString("Default");
            services.AddSingleton(new ReservoomDbContextFactory(connectionString));
        });
    }

    public static IHostBuilder AddViewModels(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton<MainViewModel>();

            services.AddTransient<MakeReservationViewModel>();
            services.AddSingleton<NavigationService<MakeReservationViewModel>>();
            services.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());

            services.AddTransient<ReservationListingViewModel>(CreateReservationListingViewModel);
            services.AddSingleton<NavigationService<ReservationListingViewModel>>();
            services.AddSingleton<Func<ReservationListingViewModel>>(s => () => s.GetRequiredService<ReservationListingViewModel>());
        });
    }

    private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
    {
        return ReservationListingViewModel.LoadViewModel(
                        services.GetRequiredService<HotelStore>(),
                        services.GetRequiredService<NavigationService<MakeReservationViewModel>>());
    }

    public static IHostBuilder AddMainWindow(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>(),
            });
        });
    }
}
