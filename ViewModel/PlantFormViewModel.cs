using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace MVVM2004PurchasingManaging.ViewModel;

public class PlantFormViewModel : BaseViewModel
{
    private readonly IPlantFormService? service;

    public PlantFormViewModel()
    {

    }
    public PlantFormViewModel(IPlantFormService plantFormService)
    {
        this.service = plantFormService;
        ListOfPlants = service.GetAll();
    }
    public ObservableCollection<Plant>? ListOfPlants { get; set; }

    public int PlantId { get; set; }
    public string? PlantName { get; set; }

    private ICommand? addPlant = null;
    public ICommand AddPlantCommand
    {
        get
        {
            if (addPlant == null)
            {
                addPlant = new RelayCommand(
                    async (o) =>
                    {
                        Plant newPlant = new() { PlantId = this.PlantId, Name = PlantName! };

                        if (!DoPlantExist(newPlant))
                        {
                            ListOfPlants = await service!.AddPlant(newPlant);
                            OnPropertyChanged(nameof(ListOfPlants));
                        }
                        else
                        {
                            MessageBox.Show($"Zaklad {newPlant.PlantId} juz istnieje!");
                        }
                    },

                   (o) =>
                   {
                       return AreTextBoxFilled();
                   });
            }

            return addPlant;
        }
    }

    private ICommand? deletePlant = null;
    public ICommand DeletePlantCommand
    {
        get
        {
            if (deletePlant == null)
            {
                deletePlant = new RelayCommand(
                    async (o) =>
                    {
                        var plant = ListOfPlants.FirstOrDefault(p => p.PlantId == this.PlantId);

                        if (plant != null)
                        {
                            ListOfPlants = await service!.RemovePlant(plant);
                            OnPropertyChanged(nameof(ListOfPlants));
                        }
                        else
                        {
                            MessageBox.Show($"Zaklad {this.PlantId} nie istnieje!");
                        }

                    },
                    (o) =>
                    {
                        return IsIdTextBoxFilled();
                    });
            }
            return deletePlant;
        }
    }

    private ICommand? editPlant = null;
    public ICommand EditPlantCommand
    {
        get
        {
            if (editPlant == null)
            {
                editPlant = new RelayCommand(
                    async (o) =>
                    {
                        Plant plant = new() { PlantId = this.PlantId, Name = this.PlantName! };

                        if (DoPlantExistByInt(PlantId))
                        {
                            ListOfPlants = await service!.EditPlant(plant);
                            OnPropertyChanged(nameof(ListOfPlants));
                        }
                        else
                        {
                            MessageBox.Show($"Zaklad {this.PlantId} nie istnieje!");
                        }

                    },
                    (o) =>
                    {
                        return AreTextBoxFilled();
                    });
            }
            return editPlant;
        }
    }

    private bool DoPlantExist(Plant newPlant) => ListOfPlants.FirstOrDefault(p => p.PlantId == newPlant.PlantId) == null ? false : true;
    private bool DoPlantExistByInt(int newPlant) => ListOfPlants.FirstOrDefault(p => p.PlantId == newPlant) == null ? false : true;
    private bool AreTextBoxFilled() => this.PlantId != 0 && this.PlantName != null && this.PlantName != "" ? true : false;
    private bool IsIdTextBoxFilled() => this.PlantId != 0 ? true : false;
}
