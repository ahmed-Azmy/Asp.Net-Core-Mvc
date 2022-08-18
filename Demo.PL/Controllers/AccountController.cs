using AutoMapper;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var Password = await userManager.CheckPasswordAsync(user, model.Password);
                    if (Password)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View(model);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordRestLink = Url.Action("ResetPassword", "Account", new { Email = user.Email, Token = Token }, Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Rest Password",
                        Body = PasswordRestLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CompleteForgetPassword));
                }
                ModelState.AddModelError(string.Empty, "Email Is Not Existed!");
            }
            return View(model);
        }

        public IActionResult CompleteForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(model);
        }
        public async new Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}
