using Microsoft.EntityFrameworkCore;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.Services
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext context;

        public OrderService(MyDbContext context)
        {
            this.context = context;
        }
        public void AddOrderPriceRecord(int indeksId, int supplierId, int plantId, decimal amount)
        {
            var priceRecord = context.IndeksPriceRecords.FirstOrDefault(i => i.IndeksId == indeksId && i.SupplierId == supplierId && i.PlantId == plantId);
            if (priceRecord == null)
            {
                MessageBox.Show("Nie ma takiego rekordu");
                return;
            }

            OrderPriceRecord orderPriceRecord = new();
            orderPriceRecord.IndeksPriceRecord = priceRecord;
            orderPriceRecord.Amount = amount;

            context.OrderPriceRecords.Add(orderPriceRecord);
            context.SaveChanges();



        }

        public void DeleteOrderPriceRecord(OrderPriceRecord record)
        {
            context.OrderPriceRecords.Remove(record);
            context.SaveChanges();
        }

        public ObservableCollection<OrderPriceRecord> GetAll()
        {
            var orderPriceRecords = context.OrderPriceRecords
                .Include(opr => opr.IndeksPriceRecord)
                .ToList();

            return orderPriceRecords.ToObservableCollection();
        }
    }
}
