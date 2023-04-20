using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.ViewModel;
using MVVM2004PurchasingManaging.Views;
using System.Windows;
namespace MVVM2004PurchasingManaging;


public partial class App : Application
{
    public static IHost? AppHost { get; private set; }
    
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
                services.AddSingleton<HomeViewModel>();
                services.AddSingleton<DatabaseViewModel>();
            })
            .Build();
    }
    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startUpForm = AppHost.Services.GetRequiredService<MainWindow>();
        startUpForm.Show();

        base.OnStartup(e);

    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }


}
