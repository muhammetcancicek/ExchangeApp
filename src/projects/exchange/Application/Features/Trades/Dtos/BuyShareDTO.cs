using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Dtos
{
    public class BuyAndSellShareDTO
    {
        public string PortfolioTitle { get; set; }
        public string ShareName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public TradeType TradeType { get; set; }

    }
}
