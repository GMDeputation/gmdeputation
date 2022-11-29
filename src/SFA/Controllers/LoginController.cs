using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SFA.Extensions;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService = null;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var sessionUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");
            if (sessionUser == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var userId = sessionUser.Id;
            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //await _userService.SaveLogOut(userId);
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [Route("validate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate([Bind("Email,Password")]LoginViewModel login, string returnUrl = null)
        {
            const string badUserNameOrPasswordMessage = "Email or password is incorrect.";
            if (login == null)
            {
                TempData["ERRMSG"] = badUserNameOrPasswordMessage;
                var vm = new LoginViewModel();
                return View("Index", vm);
            }
            var user = await _userService.Validate(login.Email, login.Password);
            if (user == null)
            {
                TempData["ERRMSG"] = badUserNameOrPasswordMessage;
                var vm = new LoginViewModel();
                return View("Index", vm);
            }

            //await _userService.SaveLogIn(user.Id);
            HttpContext.Session.Set<User>("SESSIONSFAUSER", user);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            //var sessionUser = .Session.Get<User>("SESSIONUSER");
            var sessionUser = HttpContext.Session.Get<User>("SESSIONSFAUSER");




            if (returnUrl == null)
            {
                returnUrl = TempData["returnUrl"]?.ToString();
            }
            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
