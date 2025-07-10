using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MedicineBatch
    {
        public int Id { get; set; }
        public long BatchNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long Quantity { get; set; }
        public DateTime ReceviedAt { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<Stock> stocks { get; set; } = new HashSet<Stock>();
        
    }
}
