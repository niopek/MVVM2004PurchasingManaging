using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM2004PurchasingManaging.ViewModel
{
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

        private ICommand? _goToHomeUC = null;
        private ICommand? _goToDatabaseUC = null;
        public ICommand GoToHomeUC
        {
            get
            {
                if(_goToHomeUC == null)
                {
                    _goToHomeUC = new RelayCommand(
                        (o) =>
                        {
                            CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
                        },
                        (o) =>
                        {
                            return true;
                        });
                }
                return _goToHomeUC;
            }
        }
        public ICommand GoToDatabaseUC
        {
            get
            {
                if (_goToDatabaseUC == null)
                {
                    _goToDatabaseUC = new RelayCommand(
                        (o) =>
                        {
                            CurrentViewModel = _serviceProvider.GetRequiredService<DatabaseViewModel>();
                        },
                        (o) =>
                        {
                            return true;
                        });
                }
                return _goToDatabaseUC;
            }
        }


    }
}
