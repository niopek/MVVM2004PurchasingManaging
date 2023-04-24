using Microsoft.EntityFrameworkCore;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.Services;

public class PlantFormService : IPlantFormService
{
    private readonly MyDbContext context;
    

    public PlantFormService(MyDbContext context)
    {
        this.context = context;
    }

    public async Task<ObservableCollection<Plant>?> AddPlant(Plant newPlant)
    {
        await Task.Run(() =>
        {
            context.Plants.Add(newPlant);
            context.SaveChanges();
        });
        return GetAll();
    }

    public async Task<ObservableCollection<Plant>?> RemovePlant(Plant plantToRemove)
    {
        await Task.Run(() =>
        {
            context.Plants.Remove(plantToRemove);
            context.SaveChanges();
        });
        return GetAll();
    }

    public async Task<ObservableCollection<Plant>?> EditPlant(Plant plantToUpdate)
    {
        await Task.Run(() =>
        {
            var editingPlant = context.Plants.FirstOrDefault(p => p.PlantId == plantToUpdate.PlantId);

            if (editingPlant != null)
            {
                editingPlant.Name = plantToUpdate.Name;
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Raczej nie, plant nie istnieje");
            }
        });
        return GetAll();
    }

    public ObservableCollection<Plant>? GetAll()
    {
        return context.Plants.ToObservableCollection();
    }

    
}
