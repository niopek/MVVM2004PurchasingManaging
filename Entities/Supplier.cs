using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVVM2004PurchasingManaging.Entities;

public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
}
