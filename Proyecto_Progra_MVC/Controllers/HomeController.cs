﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_Progra_MVC.Components;
using Proyecto_Progra_MVC.Contracts;
using Proyecto_Progra_MVC.Domain.Models.PlainModels;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger, ICartero cartero)
        {
            _logger = logger;
            _Cartero = cartero;

        }

            ICartero _Cartero;
            private readonly ILogger<HomeController> _logger;
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        //Cartero - Enviar correo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact([FromForm] IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _Cartero.Enviar
                    (
                        new CorreoElectronico
                        {
                            Destinatario = viewModel.correoElectronico.Destinatario,
                            Asunto = viewModel.correoElectronico.Asunto,
                            Cuerpo = viewModel.correoElectronico.Cuerpo
                        }
                    );
            }

            return View(viewModel);
        }
    }
}
