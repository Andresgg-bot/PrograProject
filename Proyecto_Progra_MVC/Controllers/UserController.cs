using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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


        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44328/api/User/getUsers"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var Response = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<User>>(Response);
                    }

                }
            }
            return View(users);
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
