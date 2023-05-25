using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Utils;
using MVVM2004PurchasingManaging.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class LoginViewModel : ObservableObject
{
    private readonly ILoginService service;
    private readonly IServiceProvider provider;
    [ObservableProperty]
    private string? _userName;
    [ObservableProperty] 
    private SecureString? _password;
    public LoginViewModel(ILoginService service, IServiceProvider provider)
	{
        this.service = service;
        this.provider = provider;
    }

    [RelayCommand]
    public void TryToLogin()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

        string? connectionString = configuration.GetConnectionString("MyConnectionString");

        var builder = new SqlConnectionStringBuilder(connectionString)
        {
            UserID = UserName,
            Password = SecureStringToString(Password)
        };

        

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            try
            {
                connection.Open();
                Util.ConnectionString = builder.ConnectionString;
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive);
                window.Hide();
                var openNewWindow = provider.GetRequiredService<MainWindow>();
                openNewWindow.Show();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

    String SecureStringToString(SecureString value)
    {
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(valuePtr);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    [RelayCommand]
    public void LoginAsGuest()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

        string? connectionString = configuration.GetConnectionString("MyConnectionString");

        var builder = new SqlConnectionStringBuilder(connectionString)
        {
            UserID = "guest1",
            Password = "Guestpassword12"
        };

        using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
        {
            try
            {
                connection.Open();
                Util.ConnectionString = builder.ConnectionString;
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive);
                window.Hide();
                var openNewWindow = provider.GetRequiredService<MainWindow>();
                openNewWindow.Show();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
