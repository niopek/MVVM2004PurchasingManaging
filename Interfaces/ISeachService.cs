using MVVM2004PurchasingManaging.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces
{
    public interface ISeachService
    {
        ObservableCollection<Indeks>? FindListOfIndeks(string text, bool isFindByNameChecked = false);
    }
}
