using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; } // Kullanıcının portföyleri
    }
}
