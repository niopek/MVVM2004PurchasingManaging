using Microsoft.VisualBasic;
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
    internal class SeachService : ISeachService
    {
        private readonly MyDbContext context;

        public SeachService(MyDbContext context)
        {
            this.context = context;
        }

        public ObservableCollection<Indeks>? FindListOfIndeks(string text, bool isFindByNameChecked = false)
        {
            if (text != null && text != "") 
            {
                ObservableCollection<Indeks> returnList = new();

                if (isFindByNameChecked)
                {
                    returnList = FindByName(text);
                    return returnList;
                }
                else
                {
                    returnList = FindById(text);
                    return returnList;
                }
                
            }
            

            return GetAll();
        }


        private ObservableCollection<Indeks> FindByName(string text)
        {
            ObservableCollection<Indeks>? returnList = new();

            if (text.Contains('*'))
            {
                string[] textSplitted = text.Split('*');

                for (int j = 0; j < textSplitted.Length; j++)
                {
                    if (j == 0)
                    {
                        returnList = context.Indekses.Where(i => i.Name.Contains(textSplitted[0])).ToObservableCollection();
                    }
                    else
                    {
                        returnList = returnList!.Where(i => i.Name.Contains(textSplitted[j])).ToObservableCollection();
                    }
                }

            }
            else
            {
                returnList = context.Indekses.Where(i => i.Name.Contains(text)).ToObservableCollection();
            }

            if (!returnList.Any())
            {
                MessageBox.Show("Brak");
            }

            return returnList;
        }

        private ObservableCollection<Indeks> FindById(string text)
        {
            ObservableCollection<Indeks>? returnList = new();
            List<int> listOfIndeksesToFind = Util.FindIndeksFromText(text);

            foreach (int id in listOfIndeksesToFind)
            {
                var indeks = context.Indekses.FirstOrDefault(indeks => indeks.Id == id);

                if (indeks != null)
                {
                    returnList.Add(indeks);
                }
            }

            if (!returnList.Any())
            {
                MessageBox.Show("Brak");
            }

            return returnList;
        }
        private ObservableCollection<Indeks>? GetAll() => context.Indekses.ToObservableCollection();
    }
}
