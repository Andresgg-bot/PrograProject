using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Application.Handlers;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.Enum;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Proyecto_Progra_MVC.Controllers
{
    public class InfoController : Controller
    {
        public InfoController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<Info>();
            _userManager = _unitOfWork.Repository<User>();
        }

        readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        readonly IRepository<Info> _repository;
        readonly IRepository<User> _userManager;

        [Authorize(Policy = PermissionTypesNames.VIEWROLES)]
        public IActionResult Index(string id)
        {
            List<Info> model = new List<Info>();

            var infoViewModel = new List<Info>();

            var query = _repository.Listar(filtro: s => s.UserId.Contains(id));

            foreach (var info in query)
            {
                var infoVM = new Info();
                infoVM.IdInfo = info.IdInfo;
                infoVM.Weight = info.Weight;
                infoVM.Height = info.Height;
                infoVM.Age = info.Age;
                infoVM.PhysicalActivity = info.PhysicalActivity;
                infoVM.Gender = info.Gender;
                infoVM.InfoDate = info.InfoDate;
                infoVM.User = _userManager.Obtener(info.UserId);

                infoViewModel.Add(infoVM);
            }

            return View(infoViewModel);
        }

        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        [HttpGet]
        public IActionResult InfoRegister(string id)
        {
            Info info = new Info();

            info.UserId = id;

            return View(info);
        }

        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InfoRegister(Info info)
        {
            if (ModelState.IsValid)
            {
                string id = info.UserId;
                info.IdInfo = 0;

                _repository.Insertar(info);

                _unitOfWork.Guardar();
                
                return RedirectToAction(nameof(Index), new { id = id });
            }
            return View(info);
        }

        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        [HttpGet]
        public IActionResult Update(int id)
        {
            Info info = new Info();

            info = _repository.Obtener(id);
            info.User = _userManager.Obtener(info.UserId);

            return View(info);
        }

        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Info info)
        {
            if (ModelState.IsValid)
            {
                string id = info.UserId;
                _repository.Actualizar(info);

                _unitOfWork.Guardar();
                return RedirectToAction(nameof(Index), new { id = id });
            }

            return View(info);
        }

        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var info = _repository.Obtener(id);

            if (info == null)
            {
                return Json(new { success = false, message = $"The information with id {id} is not found." });
            }

            _repository.Borrar(info);
            _unitOfWork.Guardar();

            return Json(new { success = true, message = "Information successfully removed." });
        }

        [Authorize(Policy = PermissionTypesNames.VIEWROLES)]
        [HttpGet]
        public IActionResult Calculate(int id)
        {
            Info info = _repository.Obtener(id);
            CalculateViewModel calculate = new CalculateViewModel();

            calculate.IdInfo = info.UserId;

            if (info.Gender.Equals(Genders.MALE))
            {
                calculate.basal = (int)(66 + (13.7 * info.Weight) + (5 * info.Height) - (6.8 * info.Age));
            }
            else
            {
                calculate.basal = (int)(655 + (9.6 * info.Weight) + (1.8 * info.Height) - (4.7 * info.Age));
            }

            if (info.PhysicalActivity.Equals(PhysicalActivity.SEDENTARY))
            {
                calculate.calorias = (int)(calculate.basal * 1.2);
                if (info.Equals(Genders.MALE))
                {
                    calculate.bajar = calculate.calorias - 337;
                    calculate.subir = calculate.calorias + 337;
                }
                else
                {
                    calculate.bajar = calculate.calorias - 293;
                    calculate.subir = calculate.calorias + 293;
                }
            }
            else if (info.PhysicalActivity.Equals(PhysicalActivity.LIGHT))
            {
                calculate.calorias = (int)(calculate.basal * 1.375);
                if (info.Equals(Genders.MALE))
                {
                    calculate.bajar = calculate.calorias - 386;
                    calculate.subir = calculate.calorias + 386;
                }
                else
                {
                    calculate.bajar = calculate.calorias - 336;
                    calculate.subir = calculate.calorias + 336;
                }
            }
            else if (info.PhysicalActivity.Equals(PhysicalActivity.MODERATE))
            {
                calculate.calorias = (int)(calculate.basal * 1.55);
                if (info.Equals(Genders.MALE))
                {
                    calculate.bajar = calculate.calorias - 435;
                    calculate.subir = calculate.calorias + 435;
                }
                else
                {
                    calculate.bajar = calculate.calorias - 378;
                    calculate.subir = calculate.calorias + 378;
                }
            }
            else if (info.PhysicalActivity.Equals(PhysicalActivity.INTENSE))
            {
                calculate.calorias = (int)(calculate.basal * 1.725);
                if (info.Equals(Genders.MALE))
                {
                    calculate.bajar = calculate.calorias - 484;
                    calculate.subir = calculate.calorias + 484;
                }
                else
                {
                    calculate.bajar = calculate.calorias - 421;
                    calculate.subir = calculate.calorias + 421;
                }
            }
            else
            {
                calculate.calorias = (int)(calculate.basal * 1.9);
                if (info.Equals(Genders.MALE))
                {
                    calculate.bajar = calculate.calorias - 533;
                    calculate.subir = calculate.calorias + 533;
                }
                else
                {
                    calculate.bajar = calculate.calorias - 463;
                    calculate.subir = calculate.calorias + 463;
                }
            }
            return View(calculate);
        }

    }
}
