using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Application.Contracts.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<User> User { get; set; }

    }
}
