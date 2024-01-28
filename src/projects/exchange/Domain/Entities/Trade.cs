using Core.Persistence.Repositories;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Trade : Entity
    {
        public int PortfolioId { get; set; }
        public int ShareId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public TradeType TradeType { get; set; }
        public virtual Portfolio Portfolio { get; set; }
        public virtual Share Share { get; set; }
    }
}
