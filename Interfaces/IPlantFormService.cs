using MVVM2004PurchasingManaging.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IPlantFormService
{
    Task<ObservableCollection<Plant>?> AddPlant(Plant newPlant);
    Task<ObservableCollection<Plant>?> RemovePlant(Plant plantToRemove);
    Task<ObservableCollection<Plant>?> EditPlant(Plant plantToUpdate);
    ObservableCollection<Plant>? GetAll();
}
