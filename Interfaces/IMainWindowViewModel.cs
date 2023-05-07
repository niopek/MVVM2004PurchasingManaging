using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IMainWindowViewModel
{
    ObservableObject CurrentViewModel { get; set; }
    void GoToHomeUC();
    void GoToIndeksFormUC();
    void GoToPlantFormUC();
    void GoToSearchUC();
    void GoToSupplierFormUC();
    void GoToIndeksPriceRecordsUC();
}
