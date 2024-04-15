using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFA.Extensions;
using SFA.Models;
using SFA.Services;

namespace SFA.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly IUserService _userService = null;
        private Boolean showPasswords = false;

        public ForgotPasswordController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var vm = new ForgotPasswordModel();
            ViewBag.ShowElement = showPasswords;
            return View(vm);
        }


        [HttpPost]
        [Route("validateEmail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateEmail(ForgotPasswordModel login, string returnUrl = null)
        {
           
            if (login.SecurityCode == null && login.NewPassword == null)
            {
                var user = await _userService.SendSecurityCode(login.Email);
                HttpContext.Session.Set<User>("SESSIONSFAUSER", user);
                var vm = new ForgotPasswordModel();
                return View("Index", vm);
            }
            else if (login.SecurityCode != String.Empty && login.Email != string.Empty && login.NewPassword == string.Empty)
            {
                var codeSentSuccessfully = _userService.ValidateSecurityCode(login.Email,login.SecurityCode);
                if(codeSentSuccessfully.IsCompleted)
                {
                    // Set the value in ViewBag
                    showPasswords = true;                 
                    var vm = new ForgotPasswordModel();
                    return View("Index", vm);
                }

            }
            else if (login.NewPassword != string.Empty && login.NewPassword == login.ConfirmPassword)
            {
                await _userService.UpdatePassword(login.Email, login.NewPassword);
                //We need to update the password and redirect to login page
                return RedirectToAction("Index", "Login");
            }


            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
