using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using Proyecto_Progra_MVC.Services.IServices;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Infraestructure.Repository.UnitOfWork;
using Proyecto_Progra_MVC.Infraestructure.Data;
using Proyecto_Progra_MVC.Infraestructure.Repository;

namespace Proyecto_Progra_MVC.Controllers
{
    public class SupplementsController : Controller
    {
        public SupplementsController(ISupplementsServices services, IWebHostEnvironment hostEnvironment)
        {
            _services = services;
            _hostEnvironment = hostEnvironment;
        }

        readonly ISupplementsServices _services;
        readonly IWebHostEnvironment _hostEnvironment;

        public async Task<IActionResult> Index()
        {
            var supplements = await _services.getSupplementsAsync();
            return View(supplements);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            Supplements sup = new Supplements();

            if (id == null || id == 0)
            {
                return View(sup);
            }
            else
            {
                sup = await _services.getSupplementById((int)id);
                return View(sup);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Supplements model, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"img\supplements");
                    var extension = Path.GetExtension(file.FileName);

                    if (model.Photo != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, model.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    model.Photo = @"\img\supplements\" + fileName + extension;

                }
                if (model.Id == 0)
                {
                    await _services.addSupplementAsync(model);
                }
                else
                {
                    await _services.updateSupplementById(model);
                }
                return RedirectToAction("Index");

            }
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSupplement(int id)
        {

            var model = await _services.getSupplementById(id);

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, model.Photo.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            await _services.deleteSupplementById(id);
            return RedirectToAction("Index");
        }
    }
}
