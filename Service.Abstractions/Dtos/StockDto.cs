using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos
{
    public class StockDto
    {
        public long Quantity { get; set; }
        public Domain.Entities.Type Type { get; set; }

        public DateTime timestamp { get; set; }
    }
}
