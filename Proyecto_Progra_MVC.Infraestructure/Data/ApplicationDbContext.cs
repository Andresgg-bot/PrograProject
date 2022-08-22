using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_MVC.Application.Contracts.Data;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne<Calories>(u => u.Calories)
                .WithOne(c => c.User)
                .HasForeignKey<Calories>(c => c.UserId);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Measures> Measures { get; set; }
        public DbSet<Info> Info { get; set; }
        public DbSet<Calories> Calories { get; set; }
    }
}
