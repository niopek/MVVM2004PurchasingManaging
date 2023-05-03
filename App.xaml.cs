using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Services;
using MVVM2004PurchasingManaging.ViewModel;
using MVVM2004PurchasingManaging.Views;
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
                options.UseSqlServer("Server=tcp:niopekdatabase.database.windows.net,1433;Initial Catalog=MVVManagingApp;Persist Security Info=False;User ID=niopek;Password=Qo2esdbaf;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
                
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<IndeksFormViewModel>();
                services.AddSingleton<SupplierFormViewModel>();
                services.AddSingleton<PlantFormViewModel>();
                services.AddSingleton<SearchViewModel>();
                services.AddSingleton<IHomeViewService, HomeViewService>();
                services.AddSingleton<IIndeksFormService, IndeksFormService>();
                services.AddSingleton<ISupplierFormService, SupplierFormService>();
                services.AddSingleton<IPlantFormService, PlantFormService>();
                services.AddSingleton<INextWindowView, BulkAddingIndeks>();
                services.AddSingleton<ISeachService, SeachService>();
            })
            .Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startUpForm = AppHost.Services.GetRequiredService<MainWindow>();
        startUpForm.Show();
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
        // Obsługa wyjątku
        MessageBox.Show("Wystąpił nieobsłużony wyjątek: " + e.Exception.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

        // Oznaczenie wyjątku jako obsłużonego
        e.Handled = true;
    }

}
