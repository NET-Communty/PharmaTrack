using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class PharmaProjectContext : DbContext
    {
        public PharmaProjectContext(DbContextOptions<PharmaProjectContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasQueryFilter(a => a.IsDeleted==false);
            builder.Entity<Medicine>()
               .HasQueryFilter(a => a.IsDeleted==false);
            builder.Entity<MedicineBatch>()
               .HasQueryFilter(a => a.IsDeleted==false);
            builder.Entity<Stock>()
               .HasQueryFilter(a => a.IsDeleted==false);
            builder.Entity<Supplier>()
               .HasQueryFilter(a => a.IsDeleted==false);
            base.OnModelCreating(builder);

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<MedicineBatch> medicineBatches { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Supplier> suppliers { get; set; }
    }
}
