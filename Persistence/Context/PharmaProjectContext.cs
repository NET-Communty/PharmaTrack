using Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class PharmaProjectContext : IdentityDbContext<ApplicationUser>
    {
        public PharmaProjectContext(DbContextOptions<PharmaProjectContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MedicineBatch>().ToTable("MedicineBatches");
            builder.Entity<ArchivedMedicineBatch>().ToTable("ArchivedMedicineBatches");
            builder.Entity<Admin>().ToTable("Admin");

            builder.Entity<ApplicationUser>()
               .HasQueryFilter(a => a.IsDeleted==false);
            builder.Entity<Category>()
                .HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<Medicine>()
               .HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<MedicineBatchBase>()
               .HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<Stock>()
               .HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<Supplier>()
               .HasQueryFilter(a => !a.IsDeleted);

            base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<MedicineBatch> medicineBatches { get; set; }
        public DbSet<ArchivedMedicineBatch> ArchivedMedicineBatches { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Supplier> suppliers { get; set; }
    }
}
