using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Trade> Trades { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .HasMany(u => u.Portfolios)
                 .WithOne(p => p.User)
                 .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Portfolio>()
                .HasMany(p => p.Trades)
                .WithOne(t => t.Portfolio)
                .HasForeignKey(t => t.PortfolioId);

            modelBuilder.Entity<Trade>()
                .HasOne(t => t.Share)
                .WithMany()
                .HasForeignKey(t => t.ShareId);

            // Kullanıcılar
            var users = new List<User>
                {
                    new User { Id = 1, Name = "Ali" },
                    new User { Id = 2, Name = "Ayşe" },
                    new User { Id = 3, Name = "Ahmet" }
                };

            // Portföyler
            var portfolios = new List<Portfolio>
                {
                    new Portfolio { Id = 1, UserId = 1 },
                    new Portfolio { Id = 2, UserId = 2 },
                    new Portfolio { Id = 3, UserId = 3 }
                };

            var shares = new List<Share>
                {
                    new Share { Id = 1, Symbol = "XYZ", Name = "XYZ Teknoloji" },
                    new Share { Id = 2, Symbol = "ABC", Name = "ABC Bankası" },
                    new Share { Id = 3, Symbol = "DEF", Name = "DEF Sigorta" }
                };

            var trades = new List<Trade>
                {
                    new Trade { Id = 1, PortfolioId = 1, ShareId = 1, TradeType = TradeType.Buy, Quantity = 10, Rate =   50.00 },
                    new Trade { Id = 2, PortfolioId = 2, ShareId = 2, TradeType = TradeType.Buy, Quantity = 20, Rate =   30.00 },
                    new Trade { Id = 3, PortfolioId = 3, ShareId = 3, TradeType = TradeType.Buy, Quantity = 15, Rate =   40.00 }
                };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Portfolio>().HasData(portfolios);
            modelBuilder.Entity<Share>().HasData(shares);
            modelBuilder.Entity<Trade>().HasData(trades);
        }
    }
}
