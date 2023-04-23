using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Windows.Input;

namespace MVVM2004PurchasingManaging.ViewModel;
[ObservableObject]
public partial class MainWindowViewModel : IMainWindowViewModel
{
    // CTORS
    public MainWindowViewModel(IServiceProvider provider)
    {
        _serviceProvider = provider;
        CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }


    // PROPERTIES
    private IServiceProvider _serviceProvider;
    private ObservableObject? _viewmodel;
    public ObservableObject CurrentViewModel
    {
        get { return _viewmodel; }
        set { SetProperty(ref _viewmodel, value); }
    }


    // FUNCTIONS

    [RelayCommand]
    public void GoToHomeUC() =>
        CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    
    [RelayCommand]
    public void GoToIndeksFormUC() =>
        CurrentViewModel = _serviceProvider.GetRequiredService<IndeksFormViewModel>();
    
    [RelayCommand]
    public void GoToSupplierFormUC() =>
        CurrentViewModel = _serviceProvider.GetRequiredService<SupplierFormViewModel>();
    
    [RelayCommand]
    public void GoToPlantFormUC() =>
        CurrentViewModel = _serviceProvider.GetRequiredService<PlantFormViewModel>(); 
    
    [RelayCommand]
    public void GoToSearchUC() =>
        CurrentViewModel = _serviceProvider.GetRequiredService<SearchViewModel>(); 
    
}
