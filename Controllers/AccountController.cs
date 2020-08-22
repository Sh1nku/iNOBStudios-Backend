using iNOBStudios.Data.Repositories;
using iNOBStudios.Models.Entities;
using iNOBStudios.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNOBStudios.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> signInManager;
        private IUserRepository userRepository;

        public AccountController(SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
        {
            this.signInManager = signInManager;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);

        }


        //Registration is disabled
        /*[HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await userRepository.CreateUser(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);

            }
            return View(model);
        }*/

        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index(string username)
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
