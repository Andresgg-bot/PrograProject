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
    public class SupplementsController : ControllerBase
    {
        public SupplementsController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _supplementsManager = _unitOfWork.Repository<Supplements>();
        }

        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Supplements> _supplementsManager;

        [HttpGet]
        [Route("getSupplements")]
        public IActionResult GetSupplements()
        {
            var allSupplements = _supplementsManager.Listar();
            _unitOfWork.Guardar();

            if (allSupplements != null)
            {
                return Ok(allSupplements);
            }
            return BadRequest("There are no supplements");
        }

        [HttpGet]
        [Route("getSupplement")]
        public IActionResult GetSupplement(int id)
        {
            var Supplement = _supplementsManager.Obtener(id);
            _unitOfWork.Guardar();

            if (Supplement != null)
            {
                return Ok(Supplement);
            }
            return BadRequest("The supplement doesnt exist");
        }

        [HttpPost]
        [Route("addSupplement")]
        public IActionResult AddSupplement([FromBody] Supplements model)
        {
            if (ModelState.IsValid)
            {
                Supplements sup = new Supplements
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Photo = model.Photo
                };

                _supplementsManager.Insertar(sup);
                _unitOfWork.Guardar();
                return Ok(sup);
            }
            return BadRequest("There was an error adding a supplement");
        }

        [HttpPut]
        [Route("updateSupplement")]
        public IActionResult UpdateSupplements([FromBody] Supplements model)
        {
            if (ModelState.IsValid)
            {
                _supplementsManager.Actualizar(model);
                _unitOfWork.Guardar();

                return Ok(model);
            }
            return BadRequest("There was an error updating the supplement");
        }

        [HttpDelete]
        [Route("deleteSupplement")]
        public IActionResult DeleteSupplement(int id)
        {
            var sup = _supplementsManager.Obtener(id);

            if(sup != null)
            {
                _supplementsManager.Borrar(sup);
                _unitOfWork.Guardar();

                return Ok($"Supplement deleted successfully");
            }

            return BadRequest($"There are no supplement with the id: {id}");
        }
    }
}
