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
                .WithMany(t => t.Trades)
                .HasForeignKey(t => t.ShareId);


            // Kullanıcılar
            var users = new List<User>
            {
                new User { Id = 1, Name = "Ali" },
                new User { Id = 2, Name = "Ayşe" },
                new User { Id = 3, Name = "Ahmet" },
                new User { Id = 4, Name = "Merve" },
                new User { Id = 5, Name = "Burak" }
            };

            // Portföyler
            var portfolios = new List<Portfolio>
            {
                new Portfolio { Id = 1, UserId = 1, Title = "Portfolio 1" },
                new Portfolio { Id = 2, UserId = 2, Title = "Portfolio 2" },
                new Portfolio { Id = 3, UserId = 3, Title = "Portfolio 3" },
                new Portfolio { Id = 4, UserId = 4, Title = "Portfolio 4" },
                new Portfolio { Id = 5, UserId = 5, Title = "Portfolio 5" }
            };

            // Hisse Senetleri
            var shares = new List<Share>
            {
                new Share { Id = 1, Symbol = "XYZ", Name = "XYZ Teknoloji" },
                new Share { Id = 2, Symbol = "ABC", Name = "ABC Bankası" },
                new Share { Id = 3, Symbol = "DEF", Name = "DEF Sigorta" },
                new Share { Id = 4, Symbol = "GHI", Name = "GHI Emlak" },
                new Share { Id = 5, Symbol = "JKL", Name = "JKL Gıda" }
            };

            var trades = new List<Trade>
            {
                new Trade { Id = 1, PortfolioId = 1, ShareId = 1, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-10) },
                new Trade { Id = 2, PortfolioId = 1, ShareId = 1, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-9) },
                new Trade { Id = 3, PortfolioId = 1, ShareId = 2, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-8) },
                new Trade { Id = 4, PortfolioId = 1, ShareId = 2, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-7) },
                new Trade { Id = 5, PortfolioId = 1, ShareId = 3, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-6) },
                new Trade { Id = 6, PortfolioId = 1, ShareId = 3, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-5) },
                new Trade { Id = 7, PortfolioId = 1, ShareId = 4, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-4) },
                new Trade { Id = 8, PortfolioId = 1, ShareId = 4, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-3) },
                new Trade { Id = 9, PortfolioId = 1, ShareId = 5, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-2) },
               new Trade { Id = 10, PortfolioId = 1, ShareId = 5, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-1) },



                new Trade { Id = 11, PortfolioId = 2, ShareId = 1, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-100) },
                new Trade { Id = 12, PortfolioId = 2, ShareId = 1, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-90) },
                new Trade { Id = 13, PortfolioId = 2, ShareId = 2, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-80) },
                new Trade { Id = 14, PortfolioId = 2, ShareId = 2, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-70) },
                new Trade { Id = 15, PortfolioId = 2, ShareId = 3, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-60) },
                new Trade { Id = 16, PortfolioId = 2, ShareId = 3, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-50) },
                new Trade { Id = 17, PortfolioId = 2, ShareId = 4, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-40) },
                new Trade { Id = 18, PortfolioId = 2, ShareId = 4, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-30) },
                new Trade { Id = 19, PortfolioId = 2, ShareId = 5, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-20) },
               new Trade { Id = 20, PortfolioId = 2, ShareId = 5, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-10) },


                new Trade { Id = 21, PortfolioId = 3, ShareId = 1, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-1000) },
                new Trade { Id = 22, PortfolioId = 3, ShareId = 1, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-900) },
                new Trade { Id = 23, PortfolioId = 3, ShareId = 2, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-800) },
                new Trade { Id = 24, PortfolioId = 3, ShareId = 2, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-700) },
                new Trade { Id = 25, PortfolioId = 3, ShareId = 3, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-600) },
                new Trade { Id = 26, PortfolioId = 3, ShareId = 3, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-500) },
                new Trade { Id = 27, PortfolioId = 3, ShareId = 4, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-400) },
                new Trade { Id = 28, PortfolioId = 3, ShareId = 4, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-300) },
                new Trade { Id = 29, PortfolioId = 3, ShareId = 5, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-200) },
               new Trade { Id = 30, PortfolioId = 3, ShareId = 5, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-100) },


                new Trade { Id = 31, PortfolioId = 4, ShareId = 1, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-10000) },
                new Trade { Id = 32, PortfolioId = 4, ShareId = 1, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-9000) },
                new Trade { Id = 33, PortfolioId = 4, ShareId = 2, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-8000) },
                new Trade { Id = 34, PortfolioId = 4, ShareId = 2, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-7000) },
                new Trade { Id = 35, PortfolioId = 4, ShareId = 3, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-6000) },
                new Trade { Id = 36, PortfolioId = 4, ShareId = 3, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-5000) },
                new Trade { Id = 37, PortfolioId = 4, ShareId = 4, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-4000) },
                new Trade { Id = 38, PortfolioId = 4, ShareId = 4, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-3000) },
                new Trade { Id = 39, PortfolioId = 4, ShareId = 5, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-2000) },
               new Trade { Id = 40, PortfolioId = 4, ShareId = 5, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-1000) },


                new Trade { Id = 41, PortfolioId = 5, ShareId = 1, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-110) },
                new Trade { Id = 42, PortfolioId = 5, ShareId = 1, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 100.00, CreateDate = DateTime.Now.AddDays(-91) },
                new Trade { Id = 43, PortfolioId = 5, ShareId = 2, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-81) },
                new Trade { Id = 44, PortfolioId = 5, ShareId = 2, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 200.00, CreateDate = DateTime.Now.AddDays(-71) },
                new Trade { Id = 45, PortfolioId = 5, ShareId = 3, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-61) },
                new Trade { Id = 46, PortfolioId = 5, ShareId = 3, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 300.00, CreateDate = DateTime.Now.AddDays(-51) },
                new Trade { Id = 47, PortfolioId = 5, ShareId = 4, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-41) },
                new Trade { Id = 48, PortfolioId = 5, ShareId = 4, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 400.00, CreateDate = DateTime.Now.AddDays(-31) },
                new Trade { Id = 49, PortfolioId = 5, ShareId = 5, TradeType = TradeType.Buy, Quantity = 100, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-21) },
               new Trade { Id = 50, PortfolioId = 5, ShareId = 5, TradeType = TradeType.Sell, Quantity = 10, UnitPrice = 500.00, CreateDate = DateTime.Now.AddDays(-11) },
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Portfolio>().HasData(portfolios);
            modelBuilder.Entity<Share>().HasData(shares);
            modelBuilder.Entity<Trade>().HasData(trades);
        }
    }
}
