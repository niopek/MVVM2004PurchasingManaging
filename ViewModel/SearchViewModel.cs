
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System.Collections.ObjectModel;
using System.Windows;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class SearchViewModel : ObservableObject
{
    private readonly ISeachService service;
    [ObservableProperty]
    private string _filterText;
    [ObservableProperty]
    private bool _isFindByNameChecked = false;
    [ObservableProperty]
    private ObservableCollection<Indeks> _listOfIndekses;
    public SearchViewModel(ISeachService service)
    {
        this.service = service;
    }
    public SearchViewModel()
    {

    }
    [RelayCommand]
    public void ShowListOfIndekses()
    {
        var list = service.FindListOfIndeks(FilterText, IsFindByNameChecked);

        if (list != null)
        {
            ListOfIndekses = list;
        }
        else
        {
            MessageBox.Show("Brak takich indeksow");
            ListOfIndekses = new();
        }
    }
}
