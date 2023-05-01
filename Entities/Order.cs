using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }
        public int SupplierId { get; set; }
        public List<IndeksPriceRecord> ListOfPriceRecords { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime Date { get; set; }

    }
}
