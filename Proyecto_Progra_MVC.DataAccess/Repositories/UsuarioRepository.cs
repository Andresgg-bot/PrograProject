using Proyecto_Progra_MVC.DataAccess.Data;
using Proyecto_Progra_MVC.DataAccess.Repository;
using Proyecto_Progra_MVC.Models.DataModels;

namespace Proyecto_Progra_MVC.DataAccess.Repositories
{
    public class UsuarioRepository : Repository<User>, IRepository<User>
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context) { }
    }
}
