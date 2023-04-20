using MVVM2004PurchasingManaging.Interfaces;

namespace MVVM2004PurchasingManaging.ViewModel;

public class HomeViewModel : BaseViewModel
{
	public HomeViewModel()
	{
        testText = "HomeViewModel";

    }
    public string testText { get; set; } 
}
