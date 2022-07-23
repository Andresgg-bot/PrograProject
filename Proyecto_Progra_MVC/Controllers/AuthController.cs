using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Proyecto_Progra_MVC.Contracts;
using Proyecto_Progra_MVC.Models.ConfigurationModels;
using Proyecto_Progra_MVC.Models.DataModels;
using Proyecto_Progra_MVC.Models.InputModels;
using Proyecto_Progra_MVC.Models.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        RecaptchaConfiguration Recaptcha;
        IRecaptchaValidator Validator;
        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;

        public AuthController(IOptions<RecaptchaConfiguration> options, IRecaptchaValidator validator,
            UserManager<User> userManager, SignInManager<User> sessionManager)
        {
            Recaptcha = options.Value;
            Validator = validator;
            _userManager = userManager;
            _signInManager = sessionManager;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            RegisterInputModel model = new RegisterInputModel();

            return View(model);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    Name = model.Name,
                    Lastname = model.Lastname,
                    Age = model.Age,
                    PhysicalActivity = model.PhysicalActivity,
                    UserName = model.Email,
                    Email = model.Email,
                    Genero = model.Genero
                };
                var result = await _userManager.CreateAsync(user, password: model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel
            {
                SiteKey = Recaptcha.SiteKey
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.InputModel.Email,
                        model.InputModel.Password, isPersistent: false, lockoutOnFailure: false);

                    var captcha = Validator.Validate(model.Recaptcha);

                    if (captcha && result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Session could not be started");
                }

            }
            catch (Exception ex)
            {
                string[] messages = ex.Message.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                foreach (var message in messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [Route("checkemail")]
        public async Task<JsonResult> CheckEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }

            return Json($"Email {email} already exist");
        }

        [HttpGet]
        [Route("forgetpassword")]
        public IActionResult ForgetPassword()
        {
            ForgetPasswordInputModel model = new ForgetPasswordInputModel();

            return View(model);
        }

        [HttpPost]
        [Route("forgetpassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordInputModel model)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;

                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPasswordUri = string.Concat
                        (
                            Url.Action(nameof(ResetPassword)),
                            "?email=",
                            WebUtility.UrlEncode(email),
                            "&token=",
                            WebUtility.UrlEncode(token)
                        );
                }

                return View(nameof(ForgetPasswordConfirmation));
            }
            return View(model);
        }

        [Route("forgetpasswordconfirmation")]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Route("resetpassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError(string.Empty, "Email and token are required");
            }
            return View();
        }

        [HttpPost]
        [Route("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    RedirectToAction("Index", "Home");
                }

                var result = await _userManager.ResetPasswordAsync
                    (user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [Route("resetpasswordconfirmation")]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
