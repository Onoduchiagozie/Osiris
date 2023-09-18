using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Osiris.Models;

namespace Osiris.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Register(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        RegisterViewModel registerViewModel = new RegisterViewModel() { };
        return View(registerViewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel rvm,string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        returnUrl = returnUrl ?? Url.Content("~/");
        if (ModelState.IsValid)
        {
            MyAppUser user = new MyAppUser(){UserName = rvm.Name,Email = rvm.Email,Name = rvm.Name};
            var box = await _userManager.CreateAsync(user,rvm.Password);
            if (box.Succeeded)
            {
               await _signInManager.SignInAsync(user,true);
                return LocalRedirect(returnUrl);
            }
        }
        return View(rvm);
    }

    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }



    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel lvm,string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        returnUrl = returnUrl ?? Url.Content("~/");
        var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, lvm.RememberMe, false);
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        { 
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt"); 
        }
        
        return View(lvm);
    }



    public IActionResult ForgotPassword()
    {
        throw new NotImplementedException();
    }
}