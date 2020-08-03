using System;
using CordaGroupTest.ExchangeRate.Entities;
using Microsoft.EntityFrameworkCore;

namespace CordaGroupTest.ExchangeRate.Data
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<CurrencyEntity> CurrencyEntities { get; set; }

        public DataContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
