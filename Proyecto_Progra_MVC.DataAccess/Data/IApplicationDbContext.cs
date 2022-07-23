using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_MVC.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.DataAccess.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<User> User { get; set; }

    }
}
