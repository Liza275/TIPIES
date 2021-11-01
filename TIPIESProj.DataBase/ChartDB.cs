using Microsoft.EntityFrameworkCore;
using System;
using TIPIESProj.DataBase.Models;

namespace TIPIESProj.DataBase
{
    public class ChartDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-V0A9OFL\SQLEXPRESS;initial catalog=BUDatabaseV2;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionLog>()
                        .HasOne(e => e.Debet)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionLog>()
                        .HasOne(e => e.Credit)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<Buyer> Buyers { set; get; }
        public virtual DbSet<ChartOfAccounts> ChartOfAccounts { set; get; }
        public virtual DbSet<Division> Divisions { set; get; }
        public virtual DbSet<OperationLog> OperationLogs { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<TransactionLog> TransactionLogs { set; get; }
    }
}
