using MVVM2004PurchasingManaging.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces;

public interface IOrderService
{
    void AddOrderPriceRecord(int indeksId, int supplierId, int plantId, decimal amount);
    void DeleteOrderPriceRecord(OrderPriceRecord record);
    ObservableCollection<OrderPriceRecord> GetAll();
}
