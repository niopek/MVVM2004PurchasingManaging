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

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plant>()
            .HasMany(p => p.IndeksPriceRecords)
            .WithOne(pr => pr.Plant)
            .HasForeignKey(pr => pr.PlantId);

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(40);
        });
    }
}
