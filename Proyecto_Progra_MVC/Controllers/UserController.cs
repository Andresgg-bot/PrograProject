using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using Proyecto_Progra_MVC.Domain.Models.Entities;

namespace Proyecto_Progra_MVC.Controllers
{
    public class UserController : Controller
    {

        public UserController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = UnitOfWork.Repository<User>();
        }

        readonly IUnitOfWork<ApplicationDbContext> UnitOfWork;
        readonly IRepository<User> Repository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile(string id)
        {
            User user = Repository.Obtener(usuario => usuario.Email == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
