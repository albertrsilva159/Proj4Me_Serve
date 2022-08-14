using System;
using Microsoft.AspNetCore.Mvc;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;

namespace Proj4Me.Web.Controllers
{
  public class PerfilController : Controller
  {
    private readonly IPerfilAppService _perfilAppService;

    public PerfilController(IPerfilAppService perfilAppService)
    {
      _perfilAppService = perfilAppService;
    }

    // GET: Perfil
    public IActionResult Index()
    {
      return View(_perfilAppService.GetAll());
    }

    // GET: Perfil/Details/5
    public IActionResult Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var perfilViewModel = _perfilAppService.GetPerfilById(id.Value);
      if (perfilViewModel == null)
      {
        return NotFound();
      }

      return View(perfilViewModel);
    }

    // GET: Perfil/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Perfil/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PerfilViewModel perfilViewModel)
    {
      if (!ModelState.IsValid) return View(perfilViewModel);

      _perfilAppService.Register(perfilViewModel);

      return View(perfilViewModel);
    }

    // GET: Perfil/Edit/5
    public IActionResult Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var perfilViewModel = _perfilAppService.GetPerfilById(id.Value);

      if (perfilViewModel == null)
      {
        return NotFound();
      }

      return View(perfilViewModel);
    }

    // POST: Perfil/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PerfilViewModel perfilViewModel)
    {
      if (!ModelState.IsValid) return View(perfilViewModel);

      _perfilAppService.Update(perfilViewModel);

      // validar se a operacao ocorreu com sucesso

      return View(perfilViewModel);
    }

    // GET: Perfil/Delete/5
    public IActionResult Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var perfilViewModel = _perfilAppService.GetPerfilById(id.Value);

      if (perfilViewModel == null)
      {
        return NotFound();
      }

      return View(perfilViewModel);
    }

    // POST: Perfil/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
      _perfilAppService.Remove(id);

      //return RedirectToAction("Index");
      return RedirectToAction(nameof(Index));
    }
  }
}
