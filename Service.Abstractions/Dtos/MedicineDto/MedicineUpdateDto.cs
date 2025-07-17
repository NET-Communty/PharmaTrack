using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos.MedicineDto
{
    public class MedicineUpdateDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "LowStockThreshold is required.")]
        public int LowStockThreshold { get; set; }
        [Required(ErrorMessage = "ImageUrl is required.")]
        public IFormFile ImageUrl { get; set; }
        
    }
}
