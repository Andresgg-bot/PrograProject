using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto_Progra_MVC.Services.IServices;

namespace Proyecto_Progra_MVC.Controllers
{
    public class UserController : Controller
    {

        public UserController(IUnitOfWork<ApplicationDbContext> unitOfWork, IUserServices services)
        {
            UnitOfWork = unitOfWork;
            _userManger = UnitOfWork.Repository<User>();
            _services = services;
        }

        readonly IUnitOfWork<ApplicationDbContext> UnitOfWork;
        readonly IRepository<User> _userManger;
        readonly IUserServices _services;


        public async Task<IActionResult> Index()
        {
            var users = await _services.getUsersAsync();
            return View(users);
        }

        public IActionResult Profile(string id)
        {
            User user = _userManger.Obtener(usuario => usuario.Email == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string? id)
        {
            User users = new User();
            if (id == null)
            {
                return View(users);
            }

            users = await _services.getUserById(id);

            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User users)
        {
            if (ModelState.IsValid)
            {
                if (users.Id != null)
                {
                    await _services.updateUserById(users);
                }

                return RedirectToAction("index", "User");
            }
            return View(users);

        }
    }
}
