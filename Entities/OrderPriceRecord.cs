using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM2004PurchasingManaging.Entities;

public class OrderPriceRecord
{
    public int OrderPriceRecordId { get; set; }
    public int IndeksId { get; set; }
    public int SupplierId { get; set; }
    public int PlantId { get; set; }
    public IndeksPriceRecord IndeksPriceRecord { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalIndeksPrice { get { return Amount * this.IndeksPriceRecord.Price; } }
    public List<OrderRecord> OrderRecords { get; set; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderPriceRecord>()
            .HasKey(pr => pr.OrderPriceRecordId);

        modelBuilder.Entity<OrderPriceRecord>()
                .HasOne(pr => pr.IndeksPriceRecord)
                .WithMany()
                .HasForeignKey(pr=> new { pr.IndeksId, pr.SupplierId, pr.PlantId })
                .HasPrincipalKey(pr=> new {pr.IndeksId, pr.SupplierId, pr.PlantId});
    }

}
