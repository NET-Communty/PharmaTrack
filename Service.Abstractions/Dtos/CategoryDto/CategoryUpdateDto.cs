﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions.Dtos.CategoryDto
{
    public class CategoryUpdateDto
    {
        
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Description is required.")]
        public string Description { get; set; }
        
    }
}
