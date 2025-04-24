using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Core.IServices;
using RentACar.Models;
using System;
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
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                ShowResetPassword = false
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string action)
        {
         
            if (action == "showResetForm")
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    ModelState.AddModelError("Email", "To reset your password you must enter your email!");
                    return View(model);
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "No account found with this email.");
                    model.ShowResetPassword = false;
                    return View(model);
                }

             
                model.ShowResetPassword = true;
                return View(model);
            }

          
            if (action == "cancelReset")
            {
                model.ShowResetPassword = false;
                model.NewPassword = null;
                model.ConfirmPassword = null;
                return View(model);
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("Password", "The Password field is required");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }

              
                if (action == "resetPassword" && !string.IsNullOrEmpty(model.NewPassword))
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                        model.ShowResetPassword = true;
                        return View(model);
                    }

                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
                    if (!resetResult.Succeeded)
                    {
                        foreach (var error in resetResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        model.ShowResetPassword = true;
                        return View(model);
                    }

                  
                    if (await _userManager.IsInRoleAsync(user, "Company"))
                    {
                        var company = await carCompanyService.GetByUserId(user.Id);
                        if (company != null)
                        {
                            HttpContext.Session.SetInt32("CompanyId", company.Id);
                            HttpContext.Session.SetString("CompanyName", company.Name);
                        }
                        await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        return RedirectToAction("Index", "Company");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "User"))
                    {
                        var userCustomer = await customerService.GetByUserId(user.Id);
                        if (userCustomer != null)
                        {
                            HttpContext.Session.SetInt32("UserId", userCustomer.Id);
                            HttpContext.Session.SetString("UserName", userCustomer.Name);
                        }
                        await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }


                if (action == "login")
                {
                    if (string.IsNullOrEmpty(model.Password))
                    {
                        ModelState.AddModelError("Password", "The password is required");
                        return View(model);
                    }
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                  
                    if (result.Succeeded)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Company"))
                        {
                            var company = await carCompanyService.GetByUserId(user.Id);
                            if (company != null)
                            {
                                HttpContext.Session.SetInt32("CompanyId", company.Id);
                                HttpContext.Session.SetString("CompanyName", company.Name);
                                return RedirectToAction("Index", "Company");
                            }
                        }
                        if (await _userManager.IsInRoleAsync(user, "User"))
                        {
                            var userCustomer = await customerService.GetByUserId(user.Id);
                            if (userCustomer != null)
                            {
                                HttpContext.Session.SetInt32("UserId", userCustomer.Id);
                                HttpContext.Session.SetString("UserName", userCustomer.Name);
                            }
                        }
                        if (model.Email == "admin@admin.com" && model.Password == "AdminPassword123!")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }
       
        public IActionResult RegisterCompany() => View();
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(RegisterCompanyViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }
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
                    await carCompanyService.Add(company);
                    await carCompanyService.Save();
                    IsRegisterCompany = true;
                    CompanyId = company.Id;
                    HttpContext.Session.SetInt32("CompanyId", company.Id);
                    HttpContext.Session.SetString("CompanyName", company.Name);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }
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
                        BirthDay = model.BirthDay,
                    };              
                    
                    await customerService.Add(customer);
                    await customerService.Save();
                    HttpContext.Session.SetInt32("UserId", customer.Id);
                    HttpContext.Session.SetString("UserName", customer.Name);
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
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

} 


