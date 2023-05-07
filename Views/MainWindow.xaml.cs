using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM2004PurchasingManaging.Views;

public partial class MainWindow : Window
{
    IMainWindowViewModel _viewModel;
    private SolidColorBrush _colorBrush;
    public MainWindow(IMainWindowViewModel viewModel)
    {
        _viewModel = viewModel;
        this.DataContext = _viewModel;
        _colorBrush = new SolidColorBrush(Color.FromRgb(61, 81, 255));
        InitializeComponent();
        HomeNav.Background = _colorBrush;
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

    private void HomeNav_Click(object sender, RoutedEventArgs e)
    {
        SetNavMenuColorToNomral();
        HomeNav.Background = _colorBrush;
    }

    private void IndeksFormNav_Click(object sender, RoutedEventArgs e)
    {
        SetNavMenuColorToNomral();
        IndeksFormNav.Background = _colorBrush;
    }

    private void SupplierFormNav_Click(object sender, RoutedEventArgs e)
    {
        SetNavMenuColorToNomral();
        SupplierFormNav.Background = _colorBrush;
    }
    private void PlantFormNav_Click(object sender, RoutedEventArgs e)
    {
        SetNavMenuColorToNomral();
        PlantFormNav.Background = _colorBrush;
    }
    private void SearchNav_Click(object sender, RoutedEventArgs e)
    {
        SetNavMenuColorToNomral();
        SearchNav.Background = _colorBrush;
    }
    private void PriceRecordsNav_Click(object sender, RoutedEventArgs e)
    {
        SetNavMenuColorToNomral();
        PriceRecordsNav.Background = _colorBrush;
    }



    private void SetNavMenuColorToNomral()
    {
        SolidColorBrush color = Brushes.SteelBlue;
        HomeNav.Background = color;
        IndeksFormNav.Background = color;
        SupplierFormNav.Background = color;
        PlantFormNav.Background = color;
        SearchNav.Background = color;
        PriceRecordsNav.Background = color;
    }


}
