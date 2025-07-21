using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos.MedicineBatchDto
{
    public class AddMedicineBatchDto
    {
        public long BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public long Quantity { get; set; }

        public DateTime ReceivedAt { get; set; }

        public int MedicineId { get; set; }

        public int SupplierId { get; set; }

    }
}
