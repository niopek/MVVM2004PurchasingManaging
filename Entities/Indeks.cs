using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MVVM2004PurchasingManaging.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVVM2004PurchasingManaging.Entities;

public class Indeks : IIndeksName
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string UnitOfMeasure { get; set; }
    public string Tc { get; set; }
    public List<IndeksPriceRecord> IndeksPriceRecords { get; set; }
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Indeks>()
            .HasMany(i => i.IndeksPriceRecords)
            .WithOne(pr => pr.Indeks)
            .HasForeignKey(pr => pr.IndeksId);
    }
}
