using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Portfolios.Dtos
{
    public class CreatePortfolioDTO
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }
}
