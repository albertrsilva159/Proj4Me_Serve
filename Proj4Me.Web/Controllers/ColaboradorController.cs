using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Proj4Me.Web.Controllers
{
  
  public class ColaboradorController : BaseController
  {
    private readonly IColaboradorAppService _colaboradorAppService;

    public ColaboradorController(IColaboradorAppService colaboradorAppService,
                                 IDomainNotificationHandler<DomainNotification> notifications,
                                 IUser user) : base(notifications, user)
    {
      _colaboradorAppService = colaboradorAppService;
    }


    [Route("Listar-Colaboradores")]
    public IActionResult Index()
    {
      return View(_colaboradorAppService.GetAll());
    }

    [Route("dados-do-colaborador/{id:guid}")]
    public IActionResult Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var colaboradorViewModel = _colaboradorAppService.GetColaboradorById(id.Value);
      if (colaboradorViewModel == null)
      {
        return NotFound();
      }

      return View(colaboradorViewModel);
    }

    [Route("novo-colaborador")]
    public IActionResult Create()
    {
      return View();
    }

     [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("novo-colaborador")]
    public IActionResult Create(ColaboradorViewModel colaboradorViewModel)
    {
      if (!ModelState.IsValid) return View(colaboradorViewModel);

      _colaboradorAppService.Register(colaboradorViewModel);

      return View(colaboradorViewModel);
    }
   
    [Route("editar-colaborador/{id:guid}")]
    public IActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var colaboradorViewModel = _colaboradorAppService.GetColaboradorById(id.Value);

      if (colaboradorViewModel == null)
      {
        return NotFound();
      }

      return View(colaboradorViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("editar-colaborador/{id:guid}")]
    public IActionResult Edit(ColaboradorViewModel colaboradorViewModel)
    {
      if (!ModelState.IsValid) return View(colaboradorViewModel);

      _colaboradorAppService.Update(colaboradorViewModel);

      // validar se a operacao ocorreu com sucesso

      return View(colaboradorViewModel);
    }

    [Route("editar-colaborador/{id:guid}")]
    public IActionResult Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var colaboradorViewModel = _colaboradorAppService.GetColaboradorById(id.Value);

      if (colaboradorViewModel == null)
      {
        return NotFound();
      }

      return View(colaboradorViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Route("editar-colaborador/{id:guid}")]
    public IActionResult DeleteConfirmed(Guid id)
    {
      _colaboradorAppService.Remove(id);

      //return RedirectToAction("Index");
      return RedirectToAction(nameof(Index));
    }
  }
}
