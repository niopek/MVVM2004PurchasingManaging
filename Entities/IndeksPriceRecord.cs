using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVVM2004PurchasingManaging.Entities
{
    public class IndeksPriceRecord
    {
        public decimal Price { get; set; }
        public string Currency { get; set; }

        // keys
        public int IndeksId { get; set; }
        public int SupplierId { get; set; }
        public int PlantId { get; set; }

        public Indeks Indeks { get; set; }
        public Supplier Supplier { get; set; }
        public Plant Plant { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndeksPriceRecord>()
                .HasKey(pr => new {pr.IndeksId, pr.SupplierId, pr.PlantId});

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

            modelBuilder.Entity<IndeksPriceRecord>(entity =>
            {
                entity.Property(pr => pr.Price)
                .IsRequired()
                .HasPrecision(18, 2);

                entity.Property(pr => pr.Currency)
                .IsRequired();
            });
        }
    }
}
