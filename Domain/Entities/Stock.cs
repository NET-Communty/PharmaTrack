using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public long Quantity { get; set; }
        public DateTime TimeStamp { get; set; }
        public Type type { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("MedicineBatchBase")]
        public int MedicineBatchBaseId { get; set; }
        public MedicineBatchBase MedicineBatchBase { get; set; }
        
    }
    public enum Type
    {
        In=1,
        Out=2,
        Expired=3,
        Returned=4
    }
}
