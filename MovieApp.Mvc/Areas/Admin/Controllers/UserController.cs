using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Entities.ComplexTypes;
using MovieApp.Entities.Concrete;
using MovieApp.Entities.Dtos;
using MovieApp.Mvc.Helpers;
using MovieApp.Mvc.Helpers.Abstract;
using MovieApp.Shared.Utilities.Extensions;
using MovieApp.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IImageHelper _imageHelper;
      
        public UserController(UserManager<User> userManager,IMapper mapper, SignInManager<User> signInManager,IImageHelper imageHelper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }
        [Authorize(Roles = "SuperAdmin,User.Read")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }
        //Get//Add User
        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ActionMode = "Add";
            return View();
        }
        //Post//Add User
        [Authorize(Roles = "SuperAdmin,User.Create")]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
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
                    UserListDto userList = new UserListDto
                    {
                        ResultStatus = ResultStatus.Success,
                        Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                        Users = await _userManager.Users.ToListAsync()
                    };
                    return RedirectToAction(nameof(Index),userList);
                }
                else
                {
                    foreach(var error in result.Errors)
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

        //Get//Update User
        [Authorize(Roles = "SuperAdmin,User.Update")]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.ActionMode = "Update";
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            var userAddDto =  _mapper.Map<UserAddDto>(user);
            return View("Add", userAddDto);
        }
        //Post//Update User
        [Authorize(Roles = "SuperAdmin,User.Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserAddDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var uploadedImageDtoResult = await _imageHelper.Upload(userUpdateDto.UserName, userUpdateDto.PictureFile, PictureType.User);
                    userUpdateDto.Picture = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageDtoResult.Data.FullName
                        : "userImages/defaultUser.png";
                    if (oldUserPicture != "userImages/defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }
                }
                var updatedUser = _mapper.Map<UserAddDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.Delete(oldUserPicture);
                    }
                    UserListDto userList = new UserListDto
                    {
                        ResultStatus = ResultStatus.Success,
                        Message = $"{updatedUser.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla güncellenmiştir.",
                        Users = await _userManager.Users.ToListAsync()
                    };
                    return RedirectToAction(nameof(Index), userList);
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
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var uploadedImageDtoResult = await _imageHelper.Upload(userUpdateDto.UserName, userUpdateDto.PictureFile, PictureType.User);
                    userUpdateDto.Picture = uploadedImageDtoResult.ResultStatus == ResultStatus.Success
                        ? uploadedImageDtoResult.Data.FullName
                        : "userImages/defaultUser.png";
                    if (oldUserPicture != "userImages/defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }

                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.Delete(oldUserPicture);
                    }
                    TempData.Add("SuccessMessage", $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDto);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(userUpdateDto);
                }

            }
            else
            {
                return View(userUpdateDto);
            }
        }
        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        TempData.Add("SuccessMessage", $"Şifreniz başarıyla değiştirilmiştir.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen, girmiş olduğunuz şu anki şifrenizi kontrol ediniz.");
                    return View(userPasswordChangeDto);
                }
            }
            else
            {
                return View(userPasswordChangeDto);
            }

            return View();
        }
        [Authorize(Roles = "SuperAdmin,User.Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (user.Picture != "userImages/defaultUser.png")
                    _imageHelper.Delete(user.Picture);
                UserListDto userList = new UserListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adlı kullanıcı adına sahip, kullanıcı başarıyla silinmiştir.",
                    Users = await _userManager.Users.ToListAsync()
                };
                return RedirectToAction(nameof(Index), userList);
            }
            else
            {
                string errorMessages = string.Empty;
                foreach(var error in result.Errors)
                {
                    errorMessages += $"*{error.Description}\n"; 
                }
                UserListDto userList = new UserListDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = "Kullanıcı silme başarısız.",
                    Users = await _userManager.Users.ToListAsync()
                };
                return RedirectToAction(nameof(Index), userList);
            }
        }
       
    }
}
