using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVVM2004PurchasingManaging.Entities;

public class OrderRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int OrderRecordId { get; set; }
    public List<OrderPriceRecord> OrderPriceRecords { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderRecord>()
            .HasKey(o => o.OrderRecordId);

        modelBuilder.Entity<OrderRecord>()
            .HasMany(o => o.OrderPriceRecords)
            .WithMany(o => o.OrderRecords);
    }


}
