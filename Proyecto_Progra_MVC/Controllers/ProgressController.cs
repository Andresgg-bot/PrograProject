using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using System;

namespace Proyecto_Progra_MVC.Controllers
{
    [Route("progress")]
    public class ProgressController : Controller
    {
        public ProgressController(<Progress> userManager)
        {
            _userManager = userManager;
        }

        readonly <Progress> _userManager;

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("register")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Create(RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                Progress progress = new()
                {
                    Id = model.Id,
                    Weight = model.Weight,
                    Height = model.Height,
                    BMI = model.BMI,
                    IdUser = model.IdUser
                };

                var result = await .CreateAsync(progress);

                if (result.Succeeded)
                {
                    await .SignInAsync(progress, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}
