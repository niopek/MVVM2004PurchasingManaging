using MVVM2004PurchasingManaging.Entities;
using System.Collections.ObjectModel;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface ISeachService
{
    ObservableCollection<Indeks>? FindListOfIndeks(string text, bool isFindByNameChecked = false);
}
