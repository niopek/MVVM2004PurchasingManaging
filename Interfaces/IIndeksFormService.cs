using CommunityToolkit.Mvvm.ComponentModel;
using MVVM2004PurchasingManaging.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IIndeksFormService
{
    Task<ObservableCollection<Indeks>?> AddIndeks(Indeks newIndeks);
    Task<ObservableCollection<Indeks>?> AddOrEditManyIndeksAsync(List<Indeks> listOfIndeks);
    Task<ObservableCollection<Indeks>?> EditIndeks(Indeks IndeksToUpdate);
    Task<ObservableCollection<Indeks>?> RemoveIndeks(Indeks IndeksToRemove);
    ObservableCollection<Indeks>? GetAll();
    void GoToBulkAddingIndeks();
}
