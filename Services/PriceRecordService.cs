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

namespace MVVM2004PurchasingManaging.Services;

public class PriceRecordService : IPriceRecordsFormService
{
    private readonly MyDbContext context;

    public PriceRecordService(MyDbContext context)
    {
        this.context = context;
    }
    public async Task AddPriceRecord(IndeksPriceRecord priceRecord)
    {
        await Task.Run(() =>
        {
            var doPriceRecord = context.IndeksPriceRecords.Any(x => x.IndeksId == priceRecord.IndeksId && x.SupplierId == priceRecord.SupplierId && x.PlantId == priceRecord.PlantId);

            if (doPriceRecord)
            {
                MessageBox.Show("Taki rekord juz istnieje!!");
                return;
            }

            var indeks = context.Indekses.Any(x => x.Id == priceRecord.IndeksId);
            var supplier = context.Suppliers.Any(x => x.SupplierId == priceRecord.SupplierId);
            var plant = context.Plants.Any(x => x.PlantId == priceRecord.PlantId);

            if (indeks == false)
            {
                MessageBox.Show($"Indeks {priceRecord.IndeksId} nie istnieje");
                return;
            }
            if (supplier == false)
            {
                MessageBox.Show($"Dostawca {priceRecord.SupplierId} nie istnieje");
                return;
            }
            if (plant == false)
            {
                MessageBox.Show($"Zaklad {priceRecord.PlantId} nie istnieje");
                return;
            }

            if (priceRecord.Price == 0 || priceRecord.Currency.Length == 0)
            {
                MessageBox.Show("Zla cena lub waluta!");
                return;
            }

            IndeksPriceRecord priceRecordToAdd = new() { IndeksId = priceRecord.IndeksId, SupplierId = priceRecord.SupplierId, PlantId = priceRecord.PlantId, Price = priceRecord.Price, Currency = priceRecord.Currency};

            context.IndeksPriceRecords.Add(priceRecordToAdd);
            context.SaveChanges();
            MessageBox.Show("Dodano nowy rekord");

        });
        
    }

    public async Task DeletePriceRecord(IndeksPriceRecord priceRecord)
    {
        await Task.Run(() =>
        {
            var isRecordIn = context.IndeksPriceRecords.FirstOrDefault(x=> x.IndeksId== priceRecord.IndeksId && x.SupplierId == priceRecord.SupplierId && x.PlantId == priceRecord.PlantId);
            if (isRecordIn == null)
            {
                MessageBox.Show("Nie ma takiego rekordu!");
                return;
            }

            context.IndeksPriceRecords.Remove(isRecordIn);
            context.SaveChanges();
            MessageBox.Show("Usunieto rekord");
        });
    }

    public async Task EditPriceRecord(IndeksPriceRecord priceRecord)
    {
        await Task.Run(() =>
        {
            var isRecordIn = context.IndeksPriceRecords.FirstOrDefault(x => x.IndeksId == priceRecord.IndeksId && x.SupplierId == priceRecord.SupplierId && x.PlantId == priceRecord.PlantId);
            if (isRecordIn == null)
            {
                MessageBox.Show("Nie ma takiego rekordu!");
                return;
            }

            isRecordIn.Price = priceRecord.Price;
            isRecordIn.Currency = priceRecord.Currency;
            context.IndeksPriceRecords.Update(isRecordIn);
            context.SaveChanges();
        });
    }

    public async Task GoToBulkAddingPriceRecord()
    {
        await Task.Run(() =>
        {
            MessageBox.Show("Masowka.... ");
        });
    }

    public ObservableCollection<IndeksPriceRecord>? GetAll() => context.IndeksPriceRecords.ToObservableCollection();
}
