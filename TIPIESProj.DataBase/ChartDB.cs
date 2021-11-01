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
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-V0A9OFL\SQLEXPRESS;initial catalog=BUDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Buyer> Buyers { set; get; }
        public virtual DbSet<ChartOfAccounts> ChartOfAccounts { set; get; }
        public virtual DbSet<Division> Divisions { set; get; }
        public virtual DbSet<OperationLog> OperationLogs { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<TransactionLog> TransactionLogs { set; get; }
    }
}
