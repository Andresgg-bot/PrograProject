using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;

namespace Proyecto_Progra_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManger = _unitOfWork.Repository<User>();
        }

        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<User> _userManger;

        [HttpGet]
        [Route("getUsers")]
        public IActionResult GetUsers()
        {
            var allUsers = _userManger.GetAll();
            _unitOfWork.Guardar();

            if (allUsers != null)
            {
                return Ok(allUsers);
            }
            return BadRequest("There are not users");
        }

        [HttpDelete]
        [Route("deleteUsers")]
        public IActionResult DeleteUsers(string id)
        {
            var User = _userManger.Obtener(id);

            if (User != null)
            {
                _userManger.Borrar(User);
                _unitOfWork.Guardar();

                return Ok($"User deleted successfully");
            }
            return BadRequest($"There are no user with the id: {id}");
        }
    }
}
