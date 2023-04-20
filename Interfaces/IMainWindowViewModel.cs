using MVVM2004PurchasingManaging.ViewModel;
using System.Windows.Input;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IMainWindowViewModel
{
    BaseViewModel CurrentViewModel { get; set; }
    ICommand GoToHomeUC { get; }
    ICommand GoToIndeksFormUC { get; }
    ICommand GoToPlantFormUC { get; }
    ICommand GoToSearchUC { get; }
    ICommand GoToSupplierFormUC { get; }
}
