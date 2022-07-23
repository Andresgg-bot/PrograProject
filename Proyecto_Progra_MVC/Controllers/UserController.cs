using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.DataAccess.Data;
using Proyecto_Progra_MVC.DataAccess.Repository;
using Proyecto_Progra_MVC.DataAccess.Repository.UnityOfWork;
using Proyecto_Progra_MVC.Models.DataModels;

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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Profile(string id)
        {
            User user = Repository.Get(filter: usuario => usuario.Email == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}
