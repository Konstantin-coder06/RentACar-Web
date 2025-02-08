using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.IServices;
using RentACar.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RentACar.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        ICarCompanyService carCompanyService;
        ICustomerService customerService;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ICarCompanyService carCompanyService,ICustomerService customerService)
        {
            this.customerService = customerService;
            this.carCompanyService = carCompanyService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        public IActionResult Login() => View();



        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }
        public IActionResult Register() => View();
        public IActionResult RegisterCompany() => View();
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Създаване на потребител
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {             
                    // Асигниране на роля към потребителя (компания)
                    await _userManager.AddToRoleAsync(user, "Company");
                    // Логване на потребителя
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var company = new CarCompany
                    {
                        Name = model.CompanyName,
                        UserId = user.Id,
                    };
                    carCompanyService.Add(company);
                    carCompanyService.Save();
                    return RedirectToAction("Index", "Home");
                }
                // При грешка при създаването на потребител
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User"); // По подразбиране новите потребители са "User" 
                                  await _signInManager.SignInAsync(user, isPersistent: false);
                    Customer customer = new Customer()
                    {
                        UserId = user.Id,
                        Name = model.Name,
                        Age = model.Age,
                    };              
                    customerService.Add(customer);
                    customerService.Save();
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

} 


