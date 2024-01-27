using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Portfolio : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
