using Microsoft.Extensions.DependencyInjection;
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Interfaces;
using MVVM2004PurchasingManaging.Utils;
using MVVM2004PurchasingManaging.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM2004PurchasingManaging.Services
{
    public class SupplierFormService : ISupplierFormService
    {
        private readonly MyDbContext context;
        private readonly IServiceProvider service;
        private readonly INextWindowView view;
        public SupplierFormService(MyDbContext context, IServiceProvider provider, INextWindowView view)
        {
            this.context = context;
            this.service = provider;
            this.view = view;
        }
        public async Task<ObservableCollection<Supplier>?> AddOrEditManySupplierAsync(List<Supplier> listOfSupplier)
        {
            await Task.Run(() =>
            {
                int numberOfEditedSuppliers = 0;
                int numberOfAddedSuppliers = 0;

                var currentDbSuppliers = context.Suppliers.ToList();
                foreach (var supplier in listOfSupplier)
                {
                    var isSupplierExistInDb = currentDbSuppliers.FirstOrDefault(i => i.SupplierId == supplier.SupplierId);

                    if (isSupplierExistInDb != null)
                    {
                        isSupplierExistInDb.SupplierName = supplier.SupplierName;

                        numberOfEditedSuppliers++;
                    }
                    else
                    {
                        if (supplier.SupplierName != null && supplier.SupplierName != "")
                        {
                            context.Suppliers.Add(supplier);
                            numberOfAddedSuppliers++;
                        }

                    }

                }
                if (numberOfEditedSuppliers > 0 || numberOfAddedSuppliers > 0)
                {
                    context.SaveChanges();
                }

                MessageBox.Show($"Dodano {numberOfAddedSuppliers} dostawców");
                MessageBox.Show($"Zaktualizowano {numberOfEditedSuppliers} dostawców");

            });

            return GetAll();
        }

        public async Task<ObservableCollection<Supplier>?> AddSupplier(Supplier newSupplier)
        {
            await Task.Run(() =>
            {
                context.Suppliers.Add(newSupplier);
                context.SaveChanges();
            });
            return GetAll();
        }

        public async Task<ObservableCollection<Supplier>?> EditSupplier(Supplier SupplierToUpdate)
        {
            await Task.Run(() =>
            {
                var editingSupplier = context.Suppliers.FirstOrDefault(s => s.SupplierId == SupplierToUpdate.SupplierId);
                if (editingSupplier != null)
                {
                    editingSupplier.SupplierName = SupplierToUpdate.SupplierName;

                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Raczej nie, dostawca nie istnieje");
                }
            });
            return GetAll();
        }

        public ObservableCollection<Supplier>? GetAll() => context.Suppliers.ToObservableCollection();

        public void GoToBulkAddingSupplier()
        {
            var model = service.GetRequiredService<SupplierFormViewModel>();
            view.Open(model);
        }

        public async Task<ObservableCollection<Supplier>?> RemoveSupplier(Supplier SupplierToRemove)
        {
            await Task.Run(() =>
            {
                context.Suppliers.Remove(SupplierToRemove);
                context.SaveChanges();
            });
            return GetAll();
        }
    }
}
