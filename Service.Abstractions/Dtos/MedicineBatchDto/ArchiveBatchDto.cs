using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos.MedicineBatchDto
{
    public class ArchiveBatchDto
    {

        public int BatchId { get; set; }
        public long BatchNumber { get; set; }
        public DateTime ArchivedAt { get; set; }
    }
}
