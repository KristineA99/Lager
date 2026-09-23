using Microsoft.EntityFrameworkCore;
using Lager.Models;

namespace Lager.DAL
{
    public class LagerDbContext : DbContext
    {
        public LagerDbContext(DbContextOptions<LagerDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

        // Versjon 1
        public DbSet<Product> Products { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }

        // Produksjon og sporing
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchParent> BatchParents { get; set; }
        public DbSet<AppleInput> AppleInputs { get; set; }
        public DbSet<BatchAddition> BatchAdditions { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<TastingNote> TastingNotes { get; set; }
        public DbSet<TankTransfer> TankTransfers { get; set; }
        public DbSet<Pasteurization> Pasteurizations { get; set; }
        public DbSet<PasteurizationReading> PasteurizationReadings { get; set; }
        public DbSet<Bottling> Bottlings { get; set; }
        public DbSet<RecipeLine> RecipeLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Blanding: sammensatt nøkkel og to koblinger til samme tabell
            modelBuilder.Entity<BatchParent>()
                .HasKey(bp => new { bp.ChildBatchId, bp.ParentBatchId });

            modelBuilder.Entity<BatchParent>()
                .HasOne(bp => bp.ChildBatch)
                .WithMany(b => b.Parents)
                .HasForeignKey(bp => bp.ChildBatchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BatchParent>()
                .HasOne(bp => bp.ParentBatch)
                .WithMany(b => b.Children)
                .HasForeignKey(bp => bp.ParentBatchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Flytting har to koblinger til Tank, så EF må få vite hvilken er hvilken
            modelBuilder.Entity<TankTransfer>()
                .HasOne(t => t.FromTank)
                .WithMany()
                .HasForeignKey(t => t.FromTankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TankTransfer>()
                .HasOne(t => t.ToTank)
                .WithMany()
                .HasForeignKey(t => t.ToTankId)
                .OnDelete(DeleteBehavior.Restrict);

            // Batchens nåværende tank
            modelBuilder.Entity<Batch>()
                .HasOne(b => b.CurrentTank)
                .WithMany(t => t.CurrentBatches)
                .HasForeignKey(b => b.CurrentTankId)
                .OnDelete(DeleteBehavior.SetNull);

            // Unike verdier: to tanker kan ikke ha samme RFID, osv.
            modelBuilder.Entity<Tank>().HasIndex(t => t.RfidTag).IsUnique();
            modelBuilder.Entity<Batch>().HasIndex(b => b.BatchCode).IsUnique();
            modelBuilder.Entity<Bottling>().HasIndex(b => b.LotNumber).IsUnique();

            // IsApproved beregnes, skal ikke lagres
            modelBuilder.Entity<Pasteurization>().Ignore(p => p.IsApproved);
        }
    }
}