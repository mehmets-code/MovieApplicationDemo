using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Authorize(Roles = "SuperAdmin,Role.Read")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(new RoleListDto
            {
                Roles = roles
            });
        }
        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpGet]
        public async Task<IActionResult> Assign(int id)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id);
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            UserRoleAssignDto userRoleAssignDto = new UserRoleAssignDto
            {
                UserId = user.Id,
                UserName = user.UserName
            };
            foreach (var role in roles)
            {
                RoleAssignDto rolesAssignDto = new RoleAssignDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    HasRole = userRoles.Contains(role.Name)
                };
                userRoleAssignDto.RoleAssignDtos.Add(rolesAssignDto);
            }

            return View(userRoleAssignDto);
        }
        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpPost]
        public async Task<IActionResult> Assign(UserRoleAssignDto userRoleAssignDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userRoleAssignDto.UserId);
                foreach (var roleAssignDto in userRoleAssignDto.RoleAssignDtos)
                {
                    if (roleAssignDto.HasRole)
                        await _userManager.AddToRoleAsync(user, roleAssignDto.RoleName);
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, roleAssignDto.RoleName);
                    }
                }
                await _userManager.UpdateSecurityStampAsync(user);
                return RedirectToAction("Index","User");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
