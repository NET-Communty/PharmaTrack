using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos.SupplierDto
{
    public class SupplierUpdateDto
    {
        [Required(ErrorMessage = "Id is required.")]

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required.")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Phone is required.")]

        public string Phone { get; set; }
    }
}
