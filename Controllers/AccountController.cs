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

        public AccountController(UserManager<CustomIdentityUser> userManager,SignInManager<CustomIdentityUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Regist()
        {
            return View();
        }
        public async Task< IActionResult> Regist(CreateRegisterUser model )
        {
            CustomIdentityUser user = new CustomIdentityUser()
            {
                Email = model.Email,
                UserName = model.Name,
                PhoneNumber = model.Phone,
              
            };
          await  _userManager.CreateAsync(user, model.Password);
          await  _signInManager.SignInAsync(user,isPersistent:false);
            return View();
        }
    }
}
