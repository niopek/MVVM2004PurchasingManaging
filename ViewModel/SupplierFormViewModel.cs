using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Services;
using MVVM2004PurchasingManaging.Utils;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class SupplierFormViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Supplier>? _listOfSuppliers = new();

    [NotifyCanExecuteChangedFor(nameof(AddSupplierCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteSupplierCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditSupplierCommand))]
    [ObservableProperty]
    private int _supplierId;
    
    [NotifyCanExecuteChangedFor(nameof(AddSupplierCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditSupplierCommand))]
    [ObservableProperty]
    private string _supplierName;
    [ObservableProperty]
    private string _path;
    private readonly ISupplierFormService service;

    public SupplierFormViewModel(ISupplierFormService service)
    {
        this.service = service;
        LoadData();
    }
    [RelayCommand(CanExecute = nameof(AreTextBoxFilled))]
    private async void AddSupplier()
    {
        Supplier newSupplier = new() { SupplierId = this.SupplierId, SupplierName = this.SupplierName };

        if (!DoSupplierExist(newSupplier))
        {
            ListOfSuppliers = await service.AddSupplier(newSupplier);
        }
        else
        {
            MessageBox.Show($"Dostawca {newSupplier.SupplierId} juz istnieje!");
        }
    }
    [RelayCommand(CanExecute = nameof(IsIdTextBoxFilled))]
    private async void DeleteSupplier()
    {
        var supplier = ListOfSuppliers.FirstOrDefault(s => s.SupplierId == this.SupplierId);
        if (supplier != null)
        {
            ListOfSuppliers = await service.RemoveSupplier(supplier);
        }
        else
        {
            MessageBox.Show($"Dostawca {this.SupplierId} nie istnieje!");
        }
    }
    [RelayCommand(CanExecute = nameof(AreTextBoxFilled))]
    private async void EditSupplier()
    {
        Supplier supplier = new() { SupplierId = this.SupplierId, SupplierName = this.SupplierName };

        if (DoSupplierExistByInt(SupplierId))
        {
            ListOfSuppliers = await service.EditSupplier(supplier);
        }
        else
        {
            MessageBox.Show($"Dostawca {this.SupplierId} nie istnieje!");
        }
    }
    [RelayCommand]
    private void GoToBulkAddingSupplier()
    {
        service.GoToBulkAddingSupplier();
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
            var listOfSupplierFromExcel = await LoadingExcelService.DataTableWithSuppliersToList(dataTable);
            ListOfSuppliers = await service.AddOrEditManySupplierAsync(listOfSupplierFromExcel);
        });


        // closing new opened window
        Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();

    }
    private async void LoadData()
    {
        var listOfSuppliers = await Task.Run(() => service.GetAll());
        if (listOfSuppliers != null)
        {
            ListOfSuppliers = listOfSuppliers;
        }
    }
    // TESTS 
    private bool DoSupplierExist(Supplier newSupplier) => ListOfSuppliers.FirstOrDefault(p => p.SupplierId == newSupplier.SupplierId) == null ? false : true;
    private bool DoSupplierExistByInt(int newSupplier) => ListOfSuppliers.FirstOrDefault(p => p.SupplierId == newSupplier) == null ? false : true;
    private bool AreTextBoxFilled() => this.SupplierId != 0 && this.SupplierName != null && this.SupplierName != "" ? true : false;
    private bool IsIdTextBoxFilled() => this.SupplierId != 0 ? true : false;
}
