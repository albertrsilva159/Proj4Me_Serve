using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;

namespace Proj4Me.Web.Controllers
{
  public class ColaboradorController : Controller
  {
    private readonly IColaboradorAppService _colaboradorAppService;

    public ColaboradorController(IColaboradorAppService colaboradorAppService)
    {
      _colaboradorAppService = colaboradorAppService;
    }

    // GET: Colaborador
    public IActionResult Index()
    {
      return View(_colaboradorAppService.GetAll());
    }

    // GET: Colaborador/Details/5
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

    // GET: Colaborador/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Colaborador/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ColaboradorViewModel colaboradorViewModel)
    {
      if (!ModelState.IsValid) return View(colaboradorViewModel);

      _colaboradorAppService.Register(colaboradorViewModel);

      return View(colaboradorViewModel);
    }

    // GET: Colaborador/Edit/5
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

    // POST: Colaborador/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ColaboradorViewModel colaboradorViewModel)
    {
      if (!ModelState.IsValid) return View(colaboradorViewModel);

      _colaboradorAppService.Update(colaboradorViewModel);

      // validar se a operacao ocorreu com sucesso

      return View(colaboradorViewModel);
    }

    // GET: Colaborador/Delete/5
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

    // POST: Colaborador/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
      _colaboradorAppService.Remove(id);

      //return RedirectToAction("Index");
      return RedirectToAction(nameof(Index));
    }
  }
}
