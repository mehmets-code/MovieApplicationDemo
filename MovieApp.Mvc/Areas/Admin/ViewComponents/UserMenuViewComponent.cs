﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MovieApp.Entities.Concrete;
using MovieApp.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
                return Content("Kullanıcı bulunamadı.");
            return View(new UserViewModel
            {
                User = user
            });
        }
    }
}
