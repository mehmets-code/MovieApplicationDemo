using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Entities.ComplexTypes;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using MovieApp.Mvc.Helpers.Abstract;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IImageHelper _imageHelper;
        private readonly IMapper _mapper;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IImageHelper imageHelper,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password,
                        userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlıştır.");
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signin(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var uploadedImageDtoResult = await _imageHelper.Upload(userAddDto.UserName, userAddDto.PictureFile, PictureType.User);
                userAddDto.Picture = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                    ? uploadedImageDtoResult.Data.FullName
                    : "userImages/defaultUser.png";
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    // return RedirectToAction("Index","Home", new { area = "" });
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    UserListDto userList = new UserListDto
                    {
                        ResultStatus = ResultStatus.Error,
                        Message = "Kullanıcı ekleme başarısız.",
                        Users = await _userManager.Users.ToListAsync()
                    };
                    return RedirectToAction(nameof(Index), userList);
                }

            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
