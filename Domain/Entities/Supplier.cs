using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Medicine> medicines { get; set; } = new HashSet<Medicine>();
        public ICollection<MedicineBatchBase> medicineBatchesBase { get; set; } = new HashSet<MedicineBatchBase>();
        
    }
}
