using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Windows.Input;

namespace MVVM2004PurchasingManaging.ViewModel;

public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
{
    private IServiceProvider _serviceProvider;
    private BaseViewModel? _viewmodel;
    public BaseViewModel CurrentViewModel
    {
        get { return _viewmodel; }
        set { SetProperty(ref _viewmodel, value); }
    }

    public MainWindowViewModel(IServiceProvider provider)
    {
        _serviceProvider = provider;
        CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }

    private ICommand? goToHomeUC = null;
    private ICommand? goToIndeksFormUC = null;
    private ICommand? goToSupplierFormUC = null;
    private ICommand? goToPlantFormUC = null;
    private ICommand? goToSearchUC = null;
    public ICommand GoToHomeUC
    {
        get
        {
            if (goToHomeUC == null)
            {
                goToHomeUC = new RelayCommand(
                    (o) =>
                    {
                        CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
                    },
                    (o) =>
                    {
                        return true;
                    });
            }
            return goToHomeUC;
        }
    }
    public ICommand GoToIndeksFormUC
    {
        get
        {
            if (goToIndeksFormUC == null)
            {
                goToIndeksFormUC = new RelayCommand(
                    (o) =>
                    {
                        CurrentViewModel = _serviceProvider.GetRequiredService<IndeksFormViewModel>();
                    },
                    (o) =>
                    {
                        return true;
                    });
            }
            return goToIndeksFormUC;
        }
    }
    public ICommand GoToSupplierFormUC
    {
        get
        {
            if (goToSupplierFormUC == null)
            {
                goToSupplierFormUC = new RelayCommand(
                    (o) =>
                    {
                        CurrentViewModel = _serviceProvider.GetRequiredService<SupplierFormViewModel>();
                    },
                    (o) =>
                    {
                        return true;
                    });
            }
            return goToSupplierFormUC;
        }
    }
    public ICommand GoToPlantFormUC
    {
        get
        {
            if (goToPlantFormUC == null)
            {
                goToPlantFormUC = new RelayCommand(
                    (o) =>
                    {
                        CurrentViewModel = (PlantFormViewModel)_serviceProvider.GetRequiredService<PlantFormViewModel>();
                    },
                    (o) =>
                    {
                        return true;
                    });
            }
            return goToPlantFormUC;
        }
    }
    public ICommand GoToSearchUC
    {
        get
        {
            if (goToSearchUC == null)
            {
                goToSearchUC = new RelayCommand(
                    (o) =>
                    {
                        CurrentViewModel = _serviceProvider.GetRequiredService<SearchViewModel>();
                    },
                    (o) =>
                    {
                        return true;
                    });
            }
            return goToSearchUC;
        }
    }


}
