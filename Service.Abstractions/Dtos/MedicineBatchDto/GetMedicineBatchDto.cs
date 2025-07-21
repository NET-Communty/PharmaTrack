using Domain.Entities;
using Service.Abstractions.Dtos.MedicineDto;
using Service.Abstractions.Dtos.SupplierDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos.MedicineBatchDto
{
    public class GetMedicineBatchDto
    {

        public long BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public long Quantity { get; set; }

        public DateTime ReceviedAt { get; set; }

        //public string MedicineName { get; set; }
        //public string SupplierName { get; set; }




    }
}
