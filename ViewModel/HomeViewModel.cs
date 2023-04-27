using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System.Collections.ObjectModel;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Post> _listOfPosts = new();
    [ObservableProperty]
    private Post _selectedPost;
    [ObservableProperty]
    private string _newPostTitle;
    [ObservableProperty]
    private string _newPostDescription;
    [ObservableProperty]
    private bool _isPostViewEnabled = true;
    [ObservableProperty]
    private bool _isAddPostFormEnabled = false;
    public HomeViewModel()
    {
        
    }

    [RelayCommand]
    public void AddPost()
    {

    }
    [RelayCommand]
    public void DeletePost()
    {

    }
    [RelayCommand]
    public void GoToAddPostForm()
    {

    }
    [RelayCommand]
    public void GoToPostView()
    {

    }


}
