using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVVM2004PurchasingManaging.Entities;

public class Plant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PlantId { get; set; }
    public string Name { get; set; }
    public List<IndeksPriceRecord> IndeksPriceRecords { get; set; }

    
}
