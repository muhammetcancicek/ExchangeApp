using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shares.Dtos
{
    public class ShareListDtoWithPrice
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double? LastBuyPrice { get; set; }
        public double? LastSellPrice { get; set; }
    }
}
