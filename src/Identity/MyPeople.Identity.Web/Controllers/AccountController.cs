using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPeople.Identity.Application;
using MyPeople.Identity.Domain.Entities;
using MyPeople.Identity.Web.Logging;
using MyPeople.Identity.Web.Models;

namespace MyPeople.Identity.Web.Controllers;

public class AccountController(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    ILogger<AccountController> logger
) : Controller
{
    private readonly ILogger<AccountController> _logger = logger;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        return View(new LoginViewModel { Input = new LoginInputModel(), ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Input.Email,
                model.Input.Password,
                model.Input.RememberMe,
                false
            );

            if (result.Succeeded)
            {
                _logger.LogInformation(LoggerEventIds.UserLogin, "User logged in.");
                return LocalRedirect(returnUrl);
            }

            // if (result.RequiresTwoFactor)
            //     return RedirectToAction(
            //         "LoginWith2fa",
            //         new { ReturnUrl = returnUrl, model.Input.RememberMe }
            //     );
            //
            // if (result.IsLockedOut)
            // {
            //     _logger.LogWarning(LoggerEventIds.UserLockout, "User account locked out.");
            //     return RedirectToAction("Lockout");
            // }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = model.Input.Email,
                Email = model.Input.Email,
            };

            var userCreatedResult = await _userManager.CreateAsync(user, model.Input.Password);

            if (userCreatedResult.Succeeded)
            {
                _logger.LogInformation(
                    LoggerEventIds.UserCreated,
                    "User created a new account with password."
                );

                await _userManager.AddToRoleAsync(user, AppRoles.User);

                // var userId = await _userManager.GetUserIdAsync(user);
                // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                // var callbackUrl = Url.Page(
                //     "/Account/ConfirmEmail",
                //     pageHandler: null,
                //     values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                //     protocol: Request.Scheme)!;

                // await _emailSender.SendEmailAsync(model.Input.Email, "Confirm your email",
                //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                // if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //     return RedirectToAction(
                //         "RegisterConfirmation",
                //         new { email = model.Input.Email, returnUrl }
                //     );

                await _signInManager.SignInAsync(user, false);
                return LocalRedirect(returnUrl);
            }

            foreach (var error in userCreatedResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
}
