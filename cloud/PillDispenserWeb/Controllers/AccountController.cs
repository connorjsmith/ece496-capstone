using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PillDispenserWeb.Models.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PillDispenserWeb.Controllers
{
    [Route("Account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            // This uses dependency injection to retrieve the UserManager and SignInManager from the IdentityFramework
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        #region Registration Routes

        // GET: /Account/Register
        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View("Register", new RegistrationViewModel());
        }


        // POST: /Account/Register
        [HttpPost("Register")]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel model, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                // Cannot register for new account if already signed in
                return RedirectToAction("Index", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion Registration Routes

        #region Login Routes

        // GET: /Account, /Account/Login
        [AllowAnonymous]
        [HttpGet("Login")]
        [HttpGet("")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                // user is already signed in, go home
                return RedirectToAction("Index", "Home");
            }
            // prompt user to signin
            return View("Login", new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> DoLogin(LoginViewModel model, string returnUrl = "/")
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("login failure", "Password or Username was Incorrect.");
            return View("Login", model);
        }

        // GET: /Account/Logout
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // return Redirect("/Account/Login");
            return View("Index");
        }

        #endregion Login Routes

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
