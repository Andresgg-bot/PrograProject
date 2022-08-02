using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using System;
using System.Linq;

namespace Proyecto_Progra_MVC.Controllers
{
    [Route("progress")]
    public class ProgressController : Controller
    {
        public ProgressController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = UnitOfWork.Repository<Progress>();
        }

        readonly IUnitOfWork<ApplicationDbContext> UnitOfWork;
        readonly IRepository<Progress> Repository;

        public IActionResult Index()
        {
            return View(Repository.Listar(ordemiento: o => o.OrderByDescending(e => e.Id)));
        }


        public IActionResult Upsert(int? id)
        {
            Progress progress = new Progress();
            if (id == null)
            {
                return View(progress);
            }

            progress = Repository.Obtener(id.GetValueOrDefault());
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Progress progress)
        {
            if (ModelState.IsValid)
            {
                if (progress.Id == 0)
                {
                    Repository.Insertar(progress);
                }
                else
                {
                    Repository.Actualizar(progress);
                }

                UnitOfWork.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(progress);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var progress = Repository.Obtener(id);

            if (progress == null)
            {
                return Json(new { success = false, message = $"No se encuentra el progreso con id {id}" });
            }

            Repository.Borrar(progress);
            UnitOfWork.Guardar();

            return Json(new { success = true, message = "Progreso borrado exitosamente." });
        }
    }
}
