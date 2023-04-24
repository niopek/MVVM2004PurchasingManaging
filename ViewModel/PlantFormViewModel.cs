using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class PlantFormViewModel : ObservableObject
{
    // PROPERTIES
    private readonly IPlantFormService? service;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlantCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeletePlantCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditPlantCommand))]
    private int _plantId;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddPlantCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditPlantCommand))]
    private string? _plantName;

    [ObservableProperty]
    private ObservableCollection<Plant>? _listOfPlants;

    // CTORS
    public PlantFormViewModel()
    {

    }
    public PlantFormViewModel(IPlantFormService plantFormService)
    {
        this.service = plantFormService;
        LoadData();
    }

    // FUNCTIONS

    [RelayCommand(CanExecute = nameof(AreTextBoxFilled))]
    private async void AddPlant()
    {
        Plant newPlant = new() { PlantId = this.PlantId, Name = PlantName! };

        if (!DoPlantExist(newPlant))
        {
            ListOfPlants = await service!.AddPlant(newPlant);
        }
        else
        {
            MessageBox.Show($"Zaklad {newPlant.PlantId} juz istnieje!");
        }
    }

    [RelayCommand(CanExecute = nameof(IsIdTextBoxFilled))]
    private async void DeletePlant()
    {
        var plant = ListOfPlants.FirstOrDefault(p => p.PlantId == this.PlantId);

        if (plant != null)
        {
            ListOfPlants = await service!.RemovePlant(plant);
        }
        else
        {
            MessageBox.Show($"Zaklad {this.PlantId} nie istnieje!");
        }
    }

    [RelayCommand(CanExecute = nameof(AreTextBoxFilled))]
    private async void EditPlant()
    {
        Plant plant = new() { PlantId = this.PlantId, Name = this.PlantName! };

        if (DoPlantExistByInt(PlantId))
        {
            ListOfPlants = await service!.EditPlant(plant);
        }
        else
        {
            MessageBox.Show($"Zaklad {this.PlantId} nie istnieje!");
        }
    }


    private async void LoadData()
    {
        var listOfPlants = await Task.Run(() => service.GetAll());
        if (listOfPlants != null)
        {
            ListOfPlants = listOfPlants;
        }
    }

    // TESTS
    private bool DoPlantExist(Plant newPlant) => ListOfPlants.FirstOrDefault(p => p.PlantId == newPlant.PlantId) == null ? false : true;
    private bool DoPlantExistByInt(int newPlant) => ListOfPlants.FirstOrDefault(p => p.PlantId == newPlant) == null ? false : true;
    private bool AreTextBoxFilled() => this.PlantId != 0 && this.PlantName != null && this.PlantName != "" ? true : false;
    private bool IsIdTextBoxFilled() => this.PlantId != 0 ? true : false;
}
