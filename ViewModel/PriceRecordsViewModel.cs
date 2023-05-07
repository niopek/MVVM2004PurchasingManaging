using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class PriceRecordsViewModel : ObservableObject
{
    // PROPERTIES
    private readonly IPriceRecordsFormService service;
    [ObservableProperty]
    private ObservableCollection<IndeksPriceRecord>? _listOfPriceRecords = new();
    [ObservableProperty]
    private int _indeksId;
    [ObservableProperty]
    private int _supplierId;
    [ObservableProperty]
    private int _plantId;
    [ObservableProperty]
    private decimal _price;
    [ObservableProperty]
    private string _currency;

    // CTORS
    public PriceRecordsViewModel()
    {

    }
    public PriceRecordsViewModel(IPriceRecordsFormService priceRecordFormService)
    {
        service = priceRecordFormService;
        LoadData();
    }

    [RelayCommand]
    private async void GoToBulkAddingPriceRecord()
    {
        await service.GoToBulkAddingPriceRecord();
    }
    [RelayCommand]
    private async void AddPriceRecord()
    {
        IndeksPriceRecord priceRecord = new() { IndeksId = this.IndeksId, SupplierId= this.SupplierId , PlantId = this.PlantId, Price = this.Price, Currency = this.Currency };
        await service.AddPriceRecord(priceRecord);
        LoadData();
    }
    [RelayCommand]
    private async void DeletePriceRecord()
    {
        IndeksPriceRecord priceRecord = new() { IndeksId = this.IndeksId, SupplierId = this.SupplierId, PlantId = this.PlantId };
        await service.DeletePriceRecord(priceRecord);
        LoadData();
    }
    [RelayCommand]
    private async void EditPriceRecord()
    {
        IndeksPriceRecord priceRecord = new() { IndeksId = this.IndeksId, SupplierId = this.SupplierId, PlantId = this.PlantId, Price = this.Price, Currency = this.Currency };
        await service.EditPriceRecord(priceRecord);
        LoadData();
    }
    private void LoadData()
    {
        ListOfPriceRecords = service.GetAll();
    }
}
