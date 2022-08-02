using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Progra_MVC.Controllers
{
    [Route("[controller]")]
    public class AdministrationController : Controller
    {
        public AdministrationController
            (RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<User> _userManager;

        public IActionResult Index()
        {
            RoleViewModel model =
                new RoleViewModel
                {
                    Roles =
                        _roleManager.Roles.Select
                            (s => new Role { Id = s.Id, Name = s.Name })
                };

            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Upsert(string id = null)
        {
            Role role = new Role();

            if (!string.IsNullOrEmpty(id))
            {
                IdentityRole identityRole = await _roleManager.FindByIdAsync(id);

                if (identityRole == null)
                {
                    return NotFound();
                }

                role.Id = identityRole.Id;
                role.Name = identityRole.Name;
            }

            return View(role);
        }

        [HttpPost]
        [Route("[action]")]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Upsert(Role model, string id = null)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();

                if (!string.IsNullOrEmpty(model.Id))
                {
                    role = await _roleManager.FindByIdAsync(model.Id);
                }

                role.Name = model.Name;

                var result =
                    !string.IsNullOrEmpty(model.Id)
                        ? await _roleManager.UpdateAsync(role)
                        : await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UsersRole(string id)
        {
            UserRoleViewModel model = new UserRoleViewModel();

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            foreach (var user in _userManager.Users)
            {
                UserRole output =
                    new UserRole
                    {
                        Email = user.Email,
                        Selected =
                            await _userManager.IsInRoleAsync(user, role.Name)
                    };
                model.Users.Add(output);
            }

            return View(model);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UsersRole(UserRoleViewModel model, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            foreach (var input in model.Users)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                IdentityResult result = null;

                if (input.Selected && !await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!input.Selected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                if (result != null && !result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            IdentityRole role = await _roleManager.FindByIdAsync(id);

            var result = await _roleManager.DeleteAsync(role);

            return Json(new { success = true, message = "Role borrado exitosamente." });
        }
    }
}
