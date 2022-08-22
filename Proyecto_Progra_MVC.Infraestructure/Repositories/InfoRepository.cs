using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Infraestructure.Repositories
{
    public class InfoRepository : Repository<Info>, IRepository<Info>
    {
        public InfoRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
