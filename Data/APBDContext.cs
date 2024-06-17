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

            modelBuilder.Entity<Client>()
                .HasKey(c => c.ClientId);
            
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.CompanyName).IsRequired(false);
                entity.Property(e => e.KRS).IsRequired(false);
                entity.Property(e => e.PESEL).IsRequired(false);
                entity.Property(e => e.CompanyName).IsRequired(false);
                entity.Property(e => e.FirstName).IsRequired(false);
                entity.Property(e => e.LastName).IsRequired(false);

                entity.HasIndex(e => e.KRS)
                    .IsUnique()
                    .HasFilter("[KRS] IS NOT NULL");

                entity.HasIndex(e => e.PESEL)
                    .IsUnique()
                    .HasFilter("[PESEL] IS NOT NULL");
            });

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

            modelBuilder.Entity<Client>().Property(c => c.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Contract>().HasIndex(c => new { c.ClientId, c.SoftwareId }).IsUnique();
        }
    }
}
