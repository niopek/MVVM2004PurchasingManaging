using MVVM2004PurchasingManaging.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface ISupplierFormService
{
    Task<ObservableCollection<Supplier>?> AddSupplier(Supplier newSupplier);
    Task<ObservableCollection<Supplier>?> AddOrEditManySupplierAsync(List<Supplier> listOfSupplier);
    Task<ObservableCollection<Supplier>?> EditSupplier(Supplier SupplierToUpdate);
    Task<ObservableCollection<Supplier>?> RemoveSupplier(Supplier SupplierToRemove);
    ObservableCollection<Supplier>? GetAll();
    void GoToBulkAddingSupplier();
}
