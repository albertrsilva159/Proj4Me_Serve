using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Infra.CrossCutting.Identity.Models;
using Proj4Me.Infra.CrossCutting.Identity.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.VisualStudio.Web.CodeGeneration;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Infra.CrossCutting.Identity.Models.AccountViewModels;
using System;
using Proj4Me.Domain.Colaboradores.Commands;
using Proj4Me.Application.ViewModels;
using Proj4Me.Application.Interfaces;

namespace Proj4Me.Web.Controllers
{
  public class AccountController : BaseController
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly IColaboradorAppService _colaboradorAppService;

    public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory,
            IDomainNotificationHandler<DomainNotification> notifications,
            IColaboradorAppService colaboradorAppService,
            IUser user) : base(notifications, user)
    {
      _userManager = userManager;
      _signInManager = signInManager;     
      _emailSender = emailSender;
      _smsSender = smsSender;
      _colaboradorAppService = colaboradorAppService;
    }

    //GET: /Account/Register
   [HttpGet]
   [AllowAnonymous]
    public IActionResult Register(string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

        //user.Claims.Add(new IdentityUserClaim<string> { ClaimType = "Eventos", ClaimValue = "Ler" });
        //user.Claims.Add(new IdentityUserClaim<string> { ClaimType = "Eventos", ClaimValue = "Gravar" });

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
          var colaborador = new ColaboradorViewModel
          {
            Id = Guid.Parse(user.Id),
            Email = user.Email,
            Nome = model.Nome         
          };

          _colaboradorAppService.Register(colaborador);

          if (!OperacaoValida())
          {
            await _userManager.DeleteAsync(user);
            return View(model);
          }

          await _signInManager.SignInAsync(user, isPersistent: false);
          //_logger.LogInformation(3, "User created a new account with password.");
          return RedirectToLocal(returnUrl);
        }
        AddErrors(result);
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }


    //GET: /Account/Login
    [HttpGet]
   [AllowAnonymous]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
      //Clear the existing external cookie to ensure a clean login process
     //await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          //_logger.LogInformation(1, "User logged in.");
          return RedirectToLocal(returnUrl);
        }
        //if (result.RequiresTwoFactor)
        //{
        //  return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //}
        if (result.IsLockedOut)
        {
          //_logger.LogWarning(2, "User account locked out.");
          return View("Lockout");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Invalid login attempt.");
          return View(model);
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      //_logger.LogInformation(4, "User logged out.");
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }
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
        return RedirectToAction(nameof(ProjetoAreaServicoController.Index), "ProjetoAreaServico");
      }
    }
    #endregion
  }
}
