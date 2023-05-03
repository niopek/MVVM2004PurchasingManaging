using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MVVM2004PurchasingManaging.Entities;

public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<IndeksPriceRecord> IndeksPriceRecords { get; set; }

   
}
