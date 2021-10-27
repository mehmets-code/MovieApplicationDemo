using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public NavbarViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var User = await _userManager.GetUserAsync(HttpContext.User);
            if (User!=null)
            {
                var userRoles = await _userManager.GetRolesAsync(User);
                return View(new UserDto
                {
                    User = User,
                    userRoles = userRoles
                });
            }

            return View(new UserDto
            {
                User = User,
                userRoles = null
            });

        }
    }
}
