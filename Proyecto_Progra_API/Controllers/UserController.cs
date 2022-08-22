using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.InputModels;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using System.Threading.Tasks;

namespace Proyecto_Progra_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = _unitOfWork.Repository<User>();
        }

        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<User> _userManager;

        [HttpGet]
        [Route("getUsers")]
        public IActionResult GetUsers()
        {
            var allUsers = _userManager.Listar();
            _unitOfWork.Guardar();

            if (allUsers != null)
            {
                return Ok(allUsers);
            }
            return BadRequest("There are not users");
        }

        [HttpGet]
        [Route("getUser")]
        public IActionResult GetUser(string id)
        {
            var User = _userManager.Obtener(id);
            _unitOfWork.Guardar();

            if (User != null)
            {
                return Ok(User);
            }
            return BadRequest("The user doesnt exist");
        }

        [HttpPut]
        [Route("updateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserInputModel model)
        {
            var user = _userManager.Obtener(model.Id);

            if (ModelState.IsValid)
            {
                user.Id = model.Id;
                user.Name = model.Name;
                user.Email = model.Email;
                user.Lastname = model.Lastname;

                _userManager.Actualizar(user);
                _unitOfWork.Guardar();

                return Ok(model);
            }

            return BadRequest("Update user failed");
        }

        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult DeleteUsers(string id)
        {
            var User = _userManager.Obtener(id);

            if (User != null)
            {
                _userManager.Borrar(User);
                _unitOfWork.Guardar();

                return Ok($"User deleted successfully");
            }
            return BadRequest($"There are no user with the id: {id}");
        }
    }
}
