using CommunityToolkit.Mvvm.ComponentModel;
using MVVM2004PurchasingManaging.Interfaces;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class HomeViewModel : ObservableObject
{
	public HomeViewModel()
	{
        testText = "HomeViewModel";

    }
    public string testText { get; set; } 
}
