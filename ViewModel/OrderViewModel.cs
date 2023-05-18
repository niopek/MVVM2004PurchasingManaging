using CommunityToolkit.Mvvm.ComponentModel;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class OrderViewModel : ObservableObject
{
    private readonly IOrderService service;
    [ObservableProperty]
    private List<OrderPriceRecord> _listOfOrderPriceRecords = new();
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
    }




}

