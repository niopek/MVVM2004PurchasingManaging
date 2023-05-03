using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace MVVM2004PurchasingManaging.Entities;

public class MyDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Indeks> Indekses { get; set; }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<IndeksPriceRecord> IndeksPriceRecords { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Indeks>()
            .HasMany(i => i.IndeksPriceRecords)
            .WithOne(pr => pr.Indeks)
            .HasForeignKey(pr => pr.IndeksId);

        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.IndeksPriceRecords)
            .WithOne(pr => pr.Supplier)
            .HasForeignKey(pr => pr.SupplierId);

        modelBuilder.Entity<Plant>()
            .HasMany(p => p.IndeksPriceRecords)
            .WithOne(pr => pr.Plant)
            .HasForeignKey(pr => pr.PlantId);

        modelBuilder.Entity<IndeksPriceRecord>()
                .HasKey(pr => new { pr.IndeksId, pr.SupplierId, pr.PlantId });

        modelBuilder.Entity<IndeksPriceRecord>()
            .HasOne(pr => pr.Indeks)
            .WithMany(ind => ind.IndeksPriceRecords)
            .HasForeignKey(pr => pr.IndeksId);

        modelBuilder.Entity<IndeksPriceRecord>()
            .HasOne(pr => pr.Supplier)
            .WithMany(sup => sup.IndeksPriceRecords)
            .HasForeignKey(pr => pr.SupplierId);

        modelBuilder.Entity<IndeksPriceRecord>()
            .HasOne(pr => pr.Plant)
            .WithMany(pla => pla.IndeksPriceRecords)
            .HasForeignKey(pr => pr.PlantId);

    }

}
