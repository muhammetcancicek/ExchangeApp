using Application.Features.Trades.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Trades.Models
{
    public class TradeListModel:BasePageableModel
    {
        public IList<TradeListDto> Items { get; set; }
    }
}
