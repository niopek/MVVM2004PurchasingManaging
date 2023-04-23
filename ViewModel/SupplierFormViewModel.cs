using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class SupplierFormViewModel : ObservableObject
{
    public SupplierFormViewModel()
    {
        testText = "SupplierFormViewModel";
    }
    public string testText { get; set; }
}
