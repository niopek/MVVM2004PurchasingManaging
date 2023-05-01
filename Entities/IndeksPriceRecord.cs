using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVVM2004PurchasingManaging.Entities
{
    [Keyless]
    public class IndeksPriceRecord
    {
        public Indeks Indeks { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
