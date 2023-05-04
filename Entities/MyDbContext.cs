using Microsoft.EntityFrameworkCore;

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
        Indeks.OnModelCreating(modelBuilder);
        Supplier.OnModelCreating(modelBuilder);
        Plant.OnModelCreating(modelBuilder);
        IndeksPriceRecord.OnModelCreating(modelBuilder);
    }

}
