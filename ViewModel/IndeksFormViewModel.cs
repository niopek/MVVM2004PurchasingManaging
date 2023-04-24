
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Utils;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MVVM2004PurchasingManaging.Interfaces;
using System.Linq;
using MVVM2004PurchasingManaging.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class IndeksFormViewModel : ObservableObject
{

    // PROPERTIES
    private readonly IIndeksFormService service;
    [ObservableProperty]
    private ObservableCollection<Indeks> _listOfIndekses;

    [NotifyCanExecuteChangedFor(nameof(AddIndeksCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteIndeksCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditIndeksCommand))]
    [ObservableProperty]
    private int _indeksId;

    [NotifyCanExecuteChangedFor(nameof(AddIndeksCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditIndeksCommand))]
    [ObservableProperty]
    private string _indeksName;

    [ObservableProperty]
    private string _indeksDescription;

    [NotifyCanExecuteChangedFor(nameof(AddIndeksCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditIndeksCommand))]
    [ObservableProperty]
    private string _unitOfMeasure;

    [NotifyCanExecuteChangedFor(nameof(AddIndeksCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditIndeksCommand))]
    [ObservableProperty]
    private string _tc;

    [ObservableProperty]
    private string _path;

    // CTORS
    public IndeksFormViewModel()
    {

    }
    public IndeksFormViewModel(IIndeksFormService indeksFormService)
    {
        service = indeksFormService;
        
    }

    // FUNCTIONS 
    [RelayCommand(CanExecute = nameof(AreTextBoxFilled))]
    private void AddIndeks()
    {
        Indeks newIndeks = new() { Id = this.IndeksId, Name = this.IndeksName, Description = this.IndeksDescription, UnitOfMeasure = this.UnitOfMeasure, Tc = this.Tc };

        if (!DoIndeksExist(newIndeks))
        {
            ListOfIndekses = service.AddIndeks(newIndeks);
        }
        else
        {
            MessageBox.Show($"Indeks {newIndeks.Id} juz istnieje!");
        }
    }

    [RelayCommand(CanExecute = nameof(IsIdTextBoxFilled))]
    private void DeleteIndeks()
    {
        var indeks = ListOfIndekses.FirstOrDefault(s => s.Id == this.IndeksId);
        if (indeks != null)
        {
            ListOfIndekses = service.RemoveIndeks(indeks);
        }
        else
        {
            MessageBox.Show($"Indeks {this.IndeksId} nie istnieje!");
        }
    }
    [RelayCommand(CanExecute = nameof(AreTextBoxFilled))]
    private void EditIndeks()
    {
        Indeks indeks = new() { Id = this.IndeksId, Name = this.IndeksName, Description = this.IndeksDescription, UnitOfMeasure = this.UnitOfMeasure, Tc = this.Tc };

        if (DoIndeksExistByInt(IndeksId))
        {
            ListOfIndekses = service.EditIndeks(indeks);
        }
        else
        {
            MessageBox.Show($"Indeks {this.IndeksId} nie istnieje!");
        }
    }

    [RelayCommand]
    private void GoToBulkAddingIndeks()
    {
        service.GoToBulkAddingIndeks();
    }
    
    [RelayCommand]
    private async void LoadFilePath()
    {
        Path = await FileNameToString.GetString();
    }

    [RelayCommand]
    private async void LoadExcel()
    {
        await Task.Run(async () =>
        {
            DataTable dataTable = await LoadingExcelService.GetDataTableFromExcel(Path);
            var listOfIndeksesFromExcel = await LoadingExcelService.DataTableWithIndeksesToList(dataTable);
            ListOfIndekses = await service.AddOrEditManyIndeksAsync(listOfIndeksesFromExcel);
        });

        // closing new opened window
        // Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();

        MessageBox.Show($"Wczytywanie zakonczone");
    }

    // TESTS 
    private bool DoIndeksExist(Indeks newIndeks) => ListOfIndekses.FirstOrDefault(p => p.Id == newIndeks.Id) == null ? false : true;
    private bool DoIndeksExistByInt(int newIndeks) => ListOfIndekses.FirstOrDefault(p => p.Id == newIndeks) == null ? false : true;
    private bool AreTextBoxFilled() => this.IndeksId != 0 && this.IndeksName != null && this.IndeksName != "" && UnitOfMeasure != null && UnitOfMeasure != "" && Tc != null && Tc != " " ? true : false;
    private bool IsIdTextBoxFilled() => this.IndeksId != 0 ? true : false;
}

