using DEMO_PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DEMO_PL.Models;
using Demo.DAL.Models;
using System.Threading.Tasks;
using System;
namespace DEMO_PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region Register
        // /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // server side validation
            {
                var user = new ApplicationUser()
                {
                    //mapping
                    // i wont use auto mapper cuz its not alot its only 5 features
                    FName =model.FName,
                    LName = model.LName,
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };
                var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
               foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);  
            }
            return View();
        }



        #endregion


        #region Login
        public IActionResult Login()
        {  return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, model.Password,
                                model.RememberMe, false);
                            if (result.Succeeded)
                                return RedirectToAction("Index", "Home");

                        }
                    ModelState.AddModelError(string.Empty, "Password is Wrong");
                }
                ModelState.AddModelError(string.Empty, "Email is not existed");
            }
            return View(); 
        }


        #endregion
    }
}
