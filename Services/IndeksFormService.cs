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
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;

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
    public ObservableCollection<Indeks> AddIndeks(Indeks newIndeks)
    {
        ObservableCollection<Indeks> ListOfIndekses;

        context.Indekses.Add(newIndeks);
        context.SaveChanges();

        ListOfIndekses = context.Indekses.ToObservableCollection();


        return ListOfIndekses;
    }

    public ObservableCollection<Indeks> RemoveIndeks(Indeks IndeksToRemove)
    {
        ObservableCollection<Indeks> ListOfIndekses;

        context.Indekses.Remove(IndeksToRemove);
        context.SaveChanges();

        ListOfIndekses = context.Indekses.ToObservableCollection();


        return ListOfIndekses;
    }

    public ObservableCollection<Indeks> EditIndeks(Indeks IndeksToUpdate)
    {
        ObservableCollection<Indeks> ListOfIndekses = new();
        try
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

            ListOfIndekses = context.Indekses.ToObservableCollection();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return ListOfIndekses;
    }

    public async Task<ObservableCollection<Indeks>> AddOrEditManyIndeksAsync(List<Indeks> listOfIndeks)
    {
        ObservableCollection<Indeks> ListOfIndekses = new();
        await Task.Run(() =>
        {
            try
            {

                var currentDbIndekses = context.Indekses.ToObservableCollection();
                var listToAdd = new List<Indeks>();
                var listToEdit = new List<Indeks>();
                foreach (var indeks in listOfIndeks)
                {
                    var isIndeksExistInDb = currentDbIndekses.FirstOrDefault(i => i.Id == indeks.Id);

                    if (isIndeksExistInDb != null)
                    {
                        listToEdit.Add(indeks);
                    }
                    else
                    {
                        listToAdd.Add(indeks);
                    }

                }

                if (listToAdd.Count() > 0)
                {
                    context.Indekses.AddRange(listToAdd);
                    context.SaveChanges();
                    MessageBox.Show($"Dodano {listToAdd.Count} indeksów");
                }
                if (listToEdit.Count() > 0)
                {
                    context.Indekses.UpdateRange(listToEdit);
                    context.SaveChanges();
                    MessageBox.Show($"Zaktualizowano {listToEdit.Count} indeksów");
                }

                ListOfIndekses = context.Indekses.ToObservableCollection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });


        return ListOfIndekses;
    }

    public ObservableCollection<Indeks> GetAll()
    {
        ObservableCollection<Indeks> ListOfIndekses;
        ListOfIndekses = context.Indekses.ToObservableCollection();
        return ListOfIndekses;
    }

    public void GoToBulkAddingIndeks()
    {
        var model = service.GetRequiredService<IndeksFormViewModel>();
        view.Open(model);

    }
}
