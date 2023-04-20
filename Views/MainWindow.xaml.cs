using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM2004PurchasingManaging.Views;

public partial class MainWindow : Window
{
    IMainWindowViewModel _viewModel;
    public MainWindow(IMainWindowViewModel viewModel)
    {
        _viewModel = viewModel;
        this.DataContext = _viewModel;
        InitializeComponent();
    }

    private void Minimalize_MouseUp(object sender, MouseButtonEventArgs e) => window.WindowState = WindowState.Minimized;


    private void Maximalize_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (window.WindowState == WindowState.Normal)
        {
            window.WindowState = WindowState.Maximized;
        }
        else
        {
            window.WindowState = WindowState.Normal;
        }
    }

    private void Close_MouseUp(object sender, MouseButtonEventArgs e) => App.Current.Shutdown();

    private void TopBorder_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void HomeNav_MouseUp(object sender, MouseButtonEventArgs e)
    {
        SetNavMenuColorToNomral();
        HomeNav.Background = new SolidColorBrush(Color.FromRgb(61, 81, 255));
    }

    private void DatabaseNav_MouseUp(object sender, MouseButtonEventArgs e)
    {
        SetNavMenuColorToNomral();
        DatabaseNav.Background = new SolidColorBrush(Color.FromRgb(61, 81, 255));
    }

    private void DocumentNav_MouseUp(object sender, MouseButtonEventArgs e)
    {
        SetNavMenuColorToNomral();
        DocumentNav.Background = new SolidColorBrush(Color.FromRgb(61, 81, 255));
    }
    private void SearchNav_MouseUp(object sender, MouseButtonEventArgs e)
    {
        SetNavMenuColorToNomral();
        SearchNav.Background = new SolidColorBrush(Color.FromRgb(61, 81, 255));
    }

    private void SettingsNav_MouseUp(object sender, MouseButtonEventArgs e)
    {
        SetNavMenuColorToNomral();
        SettingsNav.Background = new SolidColorBrush(Color.FromRgb(61, 81, 255));
    }



    private void SetNavMenuColorToNomral()
    {
        SolidColorBrush color = Brushes.SteelBlue;
        HomeNav.Background = color;
        DatabaseNav.Background = color;
        DocumentNav.Background = color;
        SearchNav.Background = color;
        SettingsNav.Background = color;
    }
}
