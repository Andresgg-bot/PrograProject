using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.Enum;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using System.Collections.Generic;

namespace Proyecto_Progra_MVC.Controllers
{
    public class MeasuresController : Controller
    {
        public MeasuresController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<Measures>();
            _userManager = _unitOfWork.Repository<User>();
        }

        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Measures> _repository;
        readonly IRepository<User> _userManager;

        public IActionResult Index(string id)
        {
            List<Measures> model = new List<Measures>();

            var measuresViewModel = new List<Measures>();

            var query = _repository.Listar(filtro: s => s.UserId.Contains(id));

            foreach (var measure in query)
            {
                var measuresVM = new Measures();
                measuresVM.IdMeasure = measure.IdMeasure;
                measuresVM.LeftArm = measure.LeftArm;
                measuresVM.RightArm = measure.RightArm;
                measuresVM.LeftLeg = measure.LeftLeg;
                measuresVM.RightLeg = measure.RightLeg;
                measuresVM.Waist = measure.Waist;
                measuresVM.Chest = measure.Chest;
                measuresVM.MeasureDate = measure.MeasureDate;
                measuresVM.User = _userManager.Obtener(measure.UserId);

                measuresViewModel.Add(measuresVM);
            }
            ViewBag.idUser = id;
            return View(measuresViewModel);
        }


        [HttpGet]
        public IActionResult MeasuresRegister(string id)
        {
            Measures measure = new Measures();

            measure.UserId = id;

            return View(measure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MeasuresRegister(Measures measure)
        {
            if (ModelState.IsValid)
            {
                string id = measure.UserId;
                measure.IdMeasure = 0;

                _repository.Insertar(measure);

                _unitOfWork.Guardar();
                
                return RedirectToAction(nameof(Index), new { id = id });
            }
            return View(measure);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Measures measure = new Measures();

            measure = _repository.Obtener(id);
            measure.User = _userManager.Obtener(measure.UserId);

            return View(measure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Measures measure)
        {
            if (ModelState.IsValid)
            {
                string id = measure.UserId;
                _repository.Actualizar(measure);

                _unitOfWork.Guardar();
                return RedirectToAction(nameof(Index), new { id = id });
            }

            return View(measure);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var measure = _repository.Obtener(id);

            if (measure == null)
            {
                return Json(new { success = false, message = $"The measures with id {id} is not found." });
            }

            _repository.Borrar(measure);
            _unitOfWork.Guardar();

            return Json(new { success = true, message = "Measures successfully removed." });
        }        

    }
}
