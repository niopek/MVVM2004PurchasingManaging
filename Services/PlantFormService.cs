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

    public async Task<ObservableCollection<Plant>> AddPlant(Plant newPlant)
    {
        List<Plant>? ListOfPlants = new();

        context.Plants.Add(newPlant);
        context.SaveChanges();

        ListOfPlants = await context.Plants.ToListAsync();

        if(ListOfPlants == null)
        {
            throw new NotImplementedException();
        }

        return ListOfPlants.ToObservableCollection()!;
    }

    public async Task<ObservableCollection<Plant>> RemovePlant(Plant plantToRemove)
    {
        List<Plant>? ListOfPlants = new();

        context.Plants.Remove(plantToRemove);
        context.SaveChanges();

        ListOfPlants = await context.Plants.ToListAsync();
        if (ListOfPlants == null)
        {
            throw new NotImplementedException();
        }

        return ListOfPlants.ToObservableCollection()!;
    }

    public async Task<ObservableCollection<Plant>> EditPlant(Plant plantToUpdate)
    {
        List<Plant>? ListOfPlants = new();

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

        ListOfPlants = await context.Plants.ToListAsync();

        if (ListOfPlants == null)
        {
            throw new NotImplementedException();
        }
        return ListOfPlants.ToObservableCollection()!;
    }

    public ObservableCollection<Plant> GetAll()
    {
        List<Plant>? ListOfPlants = new();
        ListOfPlants = context.Plants.ToList();
        if (ListOfPlants == null)
        {
            throw new NotImplementedException();
        }
        return ListOfPlants.ToObservableCollection()!;
    }

    
}
