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
        public bool IsRegisterCompany {  get; set; }
        public bool IsLoginCompany { get; set; }
        public int CompanyId {  get; set; }
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
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null && await _userManager.IsInRoleAsync(user, "Company"))
                    {
                        
                        var company = carCompanyService.GetByUserId(user.Id);
                        if (company != null)
                        {
                            HttpContext.Session.SetInt32("CompanyId", company.Id);
                        }
                    }
                    if(user!=null && await _userManager.IsInRoleAsync(user, "User"))
                    {
                        var userCustomer = customerService.GetByUserId(user.Id);
                        if (userCustomer != null)
                        {
                            HttpContext.Session.SetInt32("UserId", userCustomer.Id);
                        }
                    }
                    if (model.Email== "admin@admin.com"&& model.Password== "AdminPassword123!")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
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
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Company");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var company = new CarCompany
                    {
                        Name = model.CompanyName,
                        Description = model.Description,
                        City = model.City,
                        Country = model.Country,
                        Address = model.Address,
                        UserId = user.Id,
                    };
                    carCompanyService.Add(company);
                    carCompanyService.Save();
                    IsRegisterCompany = true;
                    CompanyId = company.Id;
                    HttpContext.Session.SetInt32("CompanyId", company.Id);
                    return RedirectToAction("Index", "Home");
                }
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
                    await _userManager.AddToRoleAsync(user, "User"); 
                                  await _signInManager.SignInAsync(user, isPersistent: false);
                    Customer customer = new Customer()
                    {
                        UserId = user.Id,
                        Name = model.Name,
                        Age = model.Age,
                    };              
                    customerService.Add(customer);
                    customerService.Save();
                    HttpContext.Session.SetInt32("UserId", customer.Id);
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


