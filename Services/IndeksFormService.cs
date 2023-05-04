using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Utils;
using MVVM2004PurchasingManaging.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.Services;

public class IndeksFormService : IIndeksFormService
{
    private readonly MyDbContext context;
    private readonly IServiceProvider service;
    private readonly INextWindowView view;
    public IndeksFormService(MyDbContext context, IServiceProvider provider, INextWindowView view)
    {
        this.context = context;
        this.service = provider;
        this.view = view;
    }
    public async Task<ObservableCollection<Indeks>?> AddIndeks(Indeks newIndeks)
    {
        await Task.Run(() => 
        {
            context.Indekses.Add(newIndeks);
            context.SaveChanges();
        });
        return GetAll();
    }

    public async Task<ObservableCollection<Indeks>?> RemoveIndeks(Indeks IndeksToRemove)
    {
        await Task.Run(() => 
        {
            context.Indekses.Remove(IndeksToRemove);
            context.SaveChanges();
        });
        return GetAll();
    }

    public async Task<ObservableCollection<Indeks>?> EditIndeks(Indeks IndeksToUpdate)
    {
        await Task.Run(() => 
        {
            var editingIndeks = context.Indekses.FirstOrDefault(s => s.Id == IndeksToUpdate.Id);
            if (editingIndeks != null)
            {
                editingIndeks.Name = IndeksToUpdate.Name;
                editingIndeks.Description = IndeksToUpdate.Description;

                if (IndeksToUpdate.UnitOfMeasure != null && IndeksToUpdate.UnitOfMeasure != "")
                    editingIndeks.UnitOfMeasure = IndeksToUpdate.UnitOfMeasure;

                if (IndeksToUpdate.Tc != null && IndeksToUpdate.Tc != "")
                    editingIndeks.Tc = IndeksToUpdate.Tc;

                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Raczej nie, indeks nie istnieje");
            }
        });
        return GetAll();
    }

    public async Task<ObservableCollection<Indeks>?> AddOrEditManyIndeksAsync(List<Indeks> listOfIndeks)
    {
        await Task.Run(() =>
        {
            int numberOfEditedIndekses = 0;
            int numberOfAddedIndekses = 0;

            var currentDbIndekses = context.Indekses.ToList();
            foreach (var indeks in listOfIndeks)
            {
                var isIndeksExistInDb = currentDbIndekses.FirstOrDefault(i => i.Id == indeks.Id);

                if (isIndeksExistInDb != null)
                {
                    isIndeksExistInDb.Name = indeks.Name;
                    isIndeksExistInDb.Description = indeks.Description;

                    if (indeks.UnitOfMeasure != null && indeks.UnitOfMeasure != "")
                        isIndeksExistInDb.UnitOfMeasure = indeks.UnitOfMeasure;

                    if (indeks.Tc != null && indeks.Tc != "")
                        isIndeksExistInDb.Tc = indeks.Tc;
                    numberOfEditedIndekses++;
                }
                else
                {
                    if (indeks.Name != null && indeks.Name != "" && indeks.UnitOfMeasure != null && indeks.UnitOfMeasure != "" && indeks.Tc != null && indeks.Tc != "")
                    {
                        context.Indekses.Add(indeks);
                        numberOfAddedIndekses++;
                    }

                }

            }
            if (numberOfEditedIndekses > 0 || numberOfAddedIndekses > 0)
            {
                context.SaveChanges();
            }

            MessageBox.Show($"Dodano {numberOfAddedIndekses} indeksów");
            MessageBox.Show($"Zaktualizowano {numberOfEditedIndekses} indeksów");

        });
        
        return GetAll();
    }

    public ObservableCollection<Indeks>? GetAll() => context.Indekses.ToObservableCollection();
    

    public void GoToBulkAddingIndeks()
    {
        var model = service.GetRequiredService<IndeksFormViewModel>();
        view.Open(model);

    }
}
