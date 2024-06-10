using APBD_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Project.Data
{
    public class APBDContext : DbContext
    {
        public APBDContext(DbContextOptions<APBDContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints

            modelBuilder.Entity<Client>()
                .HasKey(c => c.ClientId);

            modelBuilder.Entity<Software>()
                .HasKey(s => s.SoftwareId);

            modelBuilder.Entity<Contract>()
                .HasKey(c => c.ContractId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Software)
                .WithMany()
                .HasForeignKey(c => c.SoftwareId);

            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Contract)
                .WithMany()
                .HasForeignKey(p => p.ContractId);

            modelBuilder.Entity<Discount>()
                .HasKey(d => d.DiscountId);

            // Configure soft delete
            modelBuilder.Entity<Client>().Property(c => c.IsDeleted).HasDefaultValue(false);

            // Configure unique constraints
            modelBuilder.Entity<Client>().HasIndex(c => c.PESEL).IsUnique().HasFilter("[PESEL] IS NOT NULL");
            modelBuilder.Entity<Client>().HasIndex(c => c.KRS).IsUnique().HasFilter("[KRS] IS NOT NULL");
            modelBuilder.Entity<Contract>().HasIndex(c => new { c.ClientId, c.SoftwareId }).IsUnique();
        }
    }
}
