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

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.IndeksPriceRecords)
            .WithOne(pr => pr.Supplier)
            .HasForeignKey(pr => pr.SupplierId);

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(p => p.SupplierName)
            .IsRequired()
            .HasMaxLength(40);
        });
    }
}
