
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class SearchViewModel : ObservableObject
{
    public SearchViewModel()
    {
        testText = "SearchViewModel";
    }
    public string testText { get; set; }
}
