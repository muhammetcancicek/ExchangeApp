using Application.Features.Shares.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shares.Models
{
    public class ShareListModel:BasePageableModel
    {
        public IList<ShareListDtoWithPrice> Items { get; set; }
    }
}
