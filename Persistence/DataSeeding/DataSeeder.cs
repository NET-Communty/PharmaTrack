using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Persistence.DataSeeding
{
        public static class DataSeeder
        {
            public static void Seed(IServiceProvider serviceProvider)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<PharmaProjectContext>();

                    context.Database.Migrate();
                // Seed Categories
                if (!context.categories.Any())
                {
                    var categoriess = new List<Category>
    {
        new Category { Name = "Antibiotics", Description = "Medicines for bacterial infections", IsDeleted = false },
        new Category { Name = "Painkillers", Description = "Pain relief medicines", IsDeleted = false },
        new Category { Name = "Vitamins", Description = "Vitamins and supplements", IsDeleted = false },
        new Category { Name = "Allergy", Description = "Allergy treatment medicines", IsDeleted = false },
        new Category { Name = "Antidepressants", Description = "Medicines for mental health", IsDeleted = false }
    };
                    context.categories.AddRange(categoriess);
                    context.SaveChanges(); 
                }

                // Seed Suppliers
                if (!context.suppliers.Any())
                {
                    var supplierss = new List<Supplier>
    {
        new Supplier { Name = "Pharma Supplier 1", Address = "Cairo", Phone = "0100000001" },
        new Supplier { Name = "Pharma Supplier 2", Address = "Alexandria", Phone = "0100000002" },
        new Supplier { Name = "Global Health", Address = "Giza", Phone = "0100000003" },
        new Supplier { Name = "Medico Ltd.", Address = "Cairo", Phone = "0100000004" }
    };
                    context.suppliers.AddRange(supplierss);
                    context.SaveChanges(); 
                }


                // Seed Medicines
                if (!context.medicines.Any())
                {
                    var antibiotics = context.categories.FirstOrDefault(c => c.Name == "Antibiotics");
                    var painkillers = context.categories.FirstOrDefault(c => c.Name == "Painkillers");
                    var vitamins = context.categories.FirstOrDefault(c => c.Name == "Vitamins");

                    var supplier1 = context.suppliers.FirstOrDefault(s => s.Name == "Pharma Supplier 1");
                    var supplier2 = context.suppliers.FirstOrDefault(s => s.Name == "Global Health");

                    var medicines = new List<Medicine>
                    {
                        new Medicine { Name = "Amoxicillin", Description = "Broad spectrum antibiotic", CategoryId = antibiotics.Id, SupplierId = supplier1.Id, Price = 50, Quantity = 100, LowStockThreshold = 20 },
                        new Medicine { Name = "Ibuprofen", Description = "Pain relief medicine", CategoryId = painkillers.Id, SupplierId = supplier2.Id, Price = 30, Quantity = 200, LowStockThreshold = 15 },
                        new Medicine { Name = "Vitamin C", Description = "Boosts immunity", CategoryId = vitamins.Id, SupplierId = supplier1.Id, Price = 25, Quantity = 300, LowStockThreshold = 30 },
                        new Medicine { Name = "Azithromycin", Description = "Treats bacterial infections", CategoryId = antibiotics.Id, SupplierId = supplier2.Id, Price = 60, Quantity = 150, LowStockThreshold = 25 },
                        new Medicine { Name = "Paracetamol", Description = "Reduces fever and relieves pain", CategoryId = painkillers.Id, SupplierId = supplier1.Id, Price = 20, Quantity = 250, LowStockThreshold = 40 }
                    };
                    context.medicines.AddRange(medicines);
                    context.SaveChanges();
                
            }

                }
            }
        }
    }

