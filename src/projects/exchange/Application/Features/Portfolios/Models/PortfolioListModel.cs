using Application.Features.Portfolios.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Portfolios.Models
{
    public class PortfolioListModel:BasePageableModel
    {
        public IList<PortfolioListDto> Items { get; set; }
    }
}
