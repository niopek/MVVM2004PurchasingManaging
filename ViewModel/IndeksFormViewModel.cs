
using MVVM2004PurchasingManaging.Entities;
using MVVM2004PurchasingManaging.Utils;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MVVM2004PurchasingManaging.Interfaces;
using System.Linq;
using MVVM2004PurchasingManaging.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MVVM2004PurchasingManaging.ViewModel;

public partial class IndeksFormViewModel : ObservableObject
{
    private readonly IIndeksFormService service;
    public IndeksFormViewModel(IIndeksFormService indeksFormService)
    {
        
        service = indeksFormService;
        ListOfIndekses = service.GetAll();
    }
    private ObservableCollection<Indeks> _listOfIndekses { get; set; } = new();
    public ObservableCollection<Indeks> ListOfIndekses
    {
        get { return _listOfIndekses; }
        set
        {
            _listOfIndekses = value;
            OnPropertyChanged(nameof(ListOfIndekses));
        }
    }
    public int IndeksId { get; set; }
    public string IndeksName { get; set; }
    public string IndeksDescription { get; set; }
    public string UnitOfMeasure { get; set; } = "SZT";
    public string Tc { get; set; } = "TC";

    private ICommand? addIndeks = null;
    private ICommand? deleteIndeks = null;
    private ICommand? editIndeks = null;

    public ICommand AddIndeksCommand
    {
        get
        {
            if (addIndeks == null)
            {
                addIndeks = new RelayCommand(
                    (o) =>
                    {
                        Indeks newIndeks = new() { Id = this.IndeksId, Name = this.IndeksName, Description = this.IndeksDescription, UnitOfMeasure = this.UnitOfMeasure, Tc = this.Tc };

                        if (!DoIndeksExist(newIndeks))
                        {
                            ListOfIndekses = service.AddIndeks(newIndeks);
                            OnPropertyChanged(nameof(ListOfIndekses));
                        }
                        else
                        {
                            MessageBox.Show($"Indeks {newIndeks.Id} juz istnieje!");
                        }
                    },
                    (o) =>
                    {
                        return AreTextBoxFilled();
                    });
            }
            return addIndeks;
        }
    }

    public ICommand DeleteIndeksCommand
    {
        get
        {
            if (deleteIndeks == null)
            {
                deleteIndeks = new RelayCommand(
                    (o) =>
                    {
                        var indeks = ListOfIndekses.FirstOrDefault(s => s.Id == this.IndeksId);
                        if (indeks != null)
                        {
                            ListOfIndekses = service.RemoveIndeks(indeks);
                            OnPropertyChanged(nameof(ListOfIndekses));
                        }
                        else
                        {
                            MessageBox.Show($"Indeks {this.IndeksId} nie istnieje!");
                        }

                    },
                    (o) =>
                    {
                        return IsIdTextBoxFilled();
                    });
            }
            return deleteIndeks;
        }
    }

    public ICommand EditIndeksCommand
    {
        get
        {
            if (editIndeks == null)
            {
                editIndeks = new RelayCommand(
                    (o) =>
                    {
                        Indeks indeks = new() { Id = this.IndeksId, Name = this.IndeksName, Description = this.IndeksDescription, UnitOfMeasure = this.UnitOfMeasure, Tc = this.Tc };

                        if (DoIndeksExistByInt(IndeksId))
                        {
                            ListOfIndekses = service.EditIndeks(indeks);
                            OnPropertyChanged(nameof(ListOfIndekses));
                        }
                        else
                        {
                            MessageBox.Show($"Indeks {this.IndeksId} nie istnieje!");
                        }

                    },
                    (o) =>
                    {
                        return AreTextBoxFilled();
                    });
            }
            return editIndeks;
        }
    }

    private ICommand? _goToBulkAddingIndeks = null;

    public ICommand GoToBulkAddingIndeks
    {
        get
        {
            if (_goToBulkAddingIndeks == null)
            {
                _goToBulkAddingIndeks = new RelayCommand(
                    (o) =>
                    {
                       // BulkAddingIndeksWindow bulkAddingIndeksWindow = new();
                      //  bulkAddingIndeksWindow.Show();
                    },
                    (o) =>
                    {
                        return true;
                    });
            }
            return _goToBulkAddingIndeks;
        }
    }

    public string Path { get; set; }

    private ICommand? LoadFilePath = null;

    public ICommand LoadFilePathCommand
    {
        get
        {
            LoadFilePath ??= new RelayCommand(
                async (o) =>
                {
                    Path = await FileNameToString.GetString();
                    OnPropertyChanged(nameof(Path));
                });

            return LoadFilePath;
        }
    }

    private ICommand? LoadExcel = null;

    public ICommand LoadExcelCommand
    {
        get
        {
            LoadExcel ??= new RelayCommand(
                async (o) =>
                {
                    DataTable dataTable = await LoadingExcelService.GetDataTableFromExcel(Path);

                    await Task.Run(async () =>
                    {
                        var listOfIndeksesFromExcel = await LoadingExcelService.DataTableWithIndeksesToList(dataTable);
                        ListOfIndekses = await service.AddOrEditManyIndeksAsync(listOfIndeksesFromExcel);
                    });
                    Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();
                    //ListOfIndekses = LoadingDataToCollectionFromDb.GetIndekses();
                    OnPropertyChanged(nameof(ListOfIndekses));
                    MessageBox.Show($"Wczytywanie zakonczone");

                },
                (o) =>
                {
                    return true;
                });

            return LoadExcel;
        }
    }


    private bool DoIndeksExist(Indeks newIndeks) => ListOfIndekses.FirstOrDefault(p => p.Id == newIndeks.Id) == null ? false : true;
    private bool DoIndeksExistByInt(int newIndeks) => ListOfIndekses.FirstOrDefault(p => p.Id == newIndeks) == null ? false : true;
    private bool AreTextBoxFilled() => this.IndeksId != 0 && this.IndeksName != null && this.IndeksName != "" && UnitOfMeasure != null && UnitOfMeasure != "" && Tc != null && Tc != " " ? true : false;
    private bool IsIdTextBoxFilled() => this.IndeksId != 0 ? true : false;
}

