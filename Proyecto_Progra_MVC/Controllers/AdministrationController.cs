using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Progra_MVC.Application.Handlers;
using Proyecto_Progra_MVC.Domain.Models.Entities;
using Proyecto_Progra_MVC.Domain.Models.ViewModels;
using System.Collections.Generic;
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

        [Authorize(Policy = PermissionTypesNames.VIEWROLES)]
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
        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
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
        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
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
        [Authorize(Policy = PermissionTypesNames.MANAGEROLES)]
        public async Task<IActionResult> UsersRole(string id)
        {
            UserRoleViewModel model = 
                new UserRoleViewModel
                {
                    RoleId = id
                };

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
        [Authorize(Policy = PermissionTypesNames.MANAGEROLES)]
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

        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypesNames.MANAGEROLES)]
        public async Task<IActionResult> Delete(string id = null)
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
        [Authorize(Policy = PermissionTypesNames.MANAGEROLES)]
        public async Task<IActionResult> Delete(Role model, string id = null)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();

                if (!string.IsNullOrEmpty(model.Id))
                {
                    role = await _roleManager.FindByIdAsync(model.Id);
                }

                role.Name = model.Name;

                var result = !string.IsNullOrEmpty(model.Id) ? await _roleManager.DeleteAsync(role) : await _roleManager.DeleteAsync(role);
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
        [Route("userlist")]
        [Authorize(Policy = PermissionTypesNames.VIEWROLES)]
        public async Task<IActionResult> UserList()
        {

            List<UserViewModel> model = new List<UserViewModel>();

            var users = await _userManager.Users.ToListAsync();
            var userViewModel = new List<UserViewModel>();

            foreach (User user in users)
            {
                var usVM = new UserViewModel();
                usVM.Id = user.Id;
                usVM.Email = user.Email;
                usVM.Roles = new List<string>(await _userManager.GetRolesAsync(user));
                userViewModel.Add(usVM);
            }

            return View(userViewModel);
        }

        [HttpGet]
        [Route("[action]")]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewBag.Id = id;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);
            }
            ViewBag.UserName = user.Email;
            return View(model);
        }

        [HttpPost]
        [Route("[action]")]
        [Route("[action]/{id}")]
        [Authorize(Policy = PermissionTypesNames.WRITEROLES)]
        public async Task<IActionResult> EditRole(List<UserRoleViewModel> viewModel, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                return View(viewModel);
            }
            var rolesSelected = viewModel.Where(x => x.IsSelected).Select(y => y.RoleName);
            result = await _userManager.AddToRolesAsync(user, rolesSelected);

            if (!result.Succeeded)
            {
                return View(viewModel);
            }
            return RedirectToAction("UserList");
        }
    }
}
