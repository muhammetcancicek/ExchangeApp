﻿using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Share : Entity
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Trade> Trades{ get; set; }

    }
}
