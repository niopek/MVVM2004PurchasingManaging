using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Services;
using MVVM2004PurchasingManaging.Utils;
using MVVM2004PurchasingManaging.ViewModel;
using MVVM2004PurchasingManaging.Views;
using System;
using System.Windows;
using System.Windows.Threading;

namespace MVVM2004PurchasingManaging;


public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
               

                services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(Util.ConnectionString));
                services.AddScoped<LoginView>();
                services.AddScoped<LoginViewModel>();
                services.AddScoped<ILoginService, LoginService>();
                services.AddScoped<MainWindow>();
                services.AddScoped<IMainWindowViewModel, MainWindowViewModel>();
                services.AddScoped<HomeViewModel>();
                services.AddScoped<IndeksFormViewModel>();
                services.AddScoped<SupplierFormViewModel>();
                services.AddScoped<PlantFormViewModel>();
                services.AddScoped<SearchViewModel>();
                services.AddScoped<PriceRecordsViewModel>();
                services.AddScoped<OrderViewModel>();
                services.AddScoped<IHomeViewService, HomeViewService>();
                services.AddScoped<IIndeksFormService, IndeksFormService>();
                services.AddScoped<ISupplierFormService, SupplierFormService>();
                services.AddScoped<IPlantFormService, PlantFormService>();
                services.AddScoped<INextWindowView, BulkAddingIndeks>();
                services.AddScoped<ISeachService, SeachService>();
                services.AddScoped<IPriceRecordsFormService, PriceRecordService>();
                services.AddScoped<IOrderService, OrderService>();
            })
            .Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var loginTest = AppHost.Services.GetRequiredService<LoginView>();
        loginTest.Show();

        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        base.OnStartup(e);

        
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        if( e.Exception.InnerException is Microsoft.Data.SqlClient.SqlException ex)
        {
            MessageBox.Show("Brak uprawnien :)");
            e.Handled = true;
            return;
        }

        // Obsługa wyjątku
        MessageBox.Show("Wystąpił nieobsłużony wyjątek: " + e.Exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

        // Oznaczenie wyjątku jako obsłużonego
        e.Handled = true;
    }

}
