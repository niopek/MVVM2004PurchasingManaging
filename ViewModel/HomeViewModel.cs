using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class HomeViewModel : ObservableObject
{
    private readonly IHomeViewService service;
    [ObservableProperty]
    private ObservableCollection<Post>? _listOfPosts = new();
    [ObservableProperty]
    private Post? _selectedPost;
    [ObservableProperty]
    private string? _newPostTitle;
    [ObservableProperty]
    private string? _newPostDescription;
    [ObservableProperty]
    private bool _isPostViewEnabled = true;
    [ObservableProperty]
    private bool _isAddPostFormEnabled = false;
    public HomeViewModel(IHomeViewService service)
    {
        this.service = service;
        LoadData();
    }
    public HomeViewModel()
    {

    }

    [RelayCommand]
    public async void AddPost()
    {
        if(NewPostTitle != null && NewPostDescription != null)
        {
            Post post = new() { Title = NewPostTitle, Description = NewPostDescription, Date = DateTime.Now };
            ListOfPosts = await service.AddPost(post);
            SelectedPost = ListOfPosts.FirstOrDefault();
        }
    }
        
    [RelayCommand]
    public async void DeletePost()
    {
        Post? postToDelete = _selectedPost;

        if (postToDelete != null)
        {
            ListOfPosts = await service.RemovePost(postToDelete);
            SelectedPost = ListOfPosts.FirstOrDefault();
        }
        else
        {
            MessageBox.Show($"Post nie istnieje!");
        }
    }
    [RelayCommand]
    public void GoToAddPostForm()
    {
        IsPostViewEnabled = false;
        IsAddPostFormEnabled = true;
    }
    [RelayCommand]
    public void GoToPostView()
    {
        IsPostViewEnabled = true;
        IsAddPostFormEnabled = false;
    }

    private void LoadData()
    {
        ListOfPosts = service.GetAll();
        SelectedPost = ListOfPosts.FirstOrDefault();
    }
}
