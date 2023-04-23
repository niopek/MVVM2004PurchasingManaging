using CommunityToolkit.Mvvm.ComponentModel;
using MVVM2004PurchasingManaging.ViewModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IMainWindowViewModel
{
    ObservableObject CurrentViewModel { get; set; }
    void GoToHomeUC();
    void GoToIndeksFormUC();
    void GoToPlantFormUC();
    void GoToSearchUC();
    void GoToSupplierFormUC(); 
}
