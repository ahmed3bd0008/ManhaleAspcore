using ManhaleAspNetCore.ModelView.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManhaleAspNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public AccountController(UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {

            return View(_userManager.Users);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogInViewModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RemmberMe, true);
                if (result.Succeeded)
                {

                    if (ReturnUrl != null)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Manhal");
                }
                ModelState.AddModelError(string.Empty, "invaild");
            }
            return View(model);
        }
      
        [HttpGet]
        public IActionResult Regist()
        {
            return View();
        }
        public async Task< IActionResult> Regist(CreateRegisterUser model )
        {
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new 
                     CustomIdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Name,
                    PhoneNumber = model.Phone,

                };
              var result=  await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Logout()
        {
            await _signInManager.SignOutAsync();
                return RedirectToAction("Account","Index");
        }

    }

}
