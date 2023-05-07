using MVVM2004PurchasingManaging.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Interfaces
{
    public interface IPriceRecordsFormService
    {
        Task GoToBulkAddingPriceRecord();
        Task AddPriceRecord(IndeksPriceRecord priceRecord);
        Task DeletePriceRecord(IndeksPriceRecord priceRecord);
        Task EditPriceRecord(IndeksPriceRecord priceRecord);
        ObservableCollection<IndeksPriceRecord>? GetAll();
    }
}
