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

public partial class OrderViewModel : ObservableObject
{
    private readonly IOrderService service;
    [ObservableProperty]
    private ObservableCollection<OrderPriceRecord> _listOfOrderPriceRecords = new();
    [ObservableProperty]
    private int _indeksId;
    [ObservableProperty]
    private int _supplierId;
    [ObservableProperty]
    private int _plantId;
    [ObservableProperty]
    private decimal _amount;
    [ObservableProperty]
    private decimal _totalPrice;

    public OrderViewModel()
    {

    }

    public OrderViewModel(IOrderService service)
    {
        this.service = service;
        ListOfOrderPriceRecords = service.GetAll();
    }

    [RelayCommand]
    private void AddOrderPriceRecord()
    {
        var indeksId = IndeksId;
        var supplierId = SupplierId;
        var plantId = PlantId;
        var amount = Amount;

        service.AddOrderPriceRecord(indeksId, supplierId, plantId, amount);
        ListOfOrderPriceRecords = service.GetAll();

    }

}

