using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;

namespace Proj4Me.Web.Controllers
{
  public class ProjetoAreaServicoController : Controller
  {
    private readonly IProjetoAreaServicoAppService _projetoAreaServicoAppService;
    private readonly IColaboradorAppService _colaboradorAppService;
    private readonly IPerfilAppService _perfilAppService;

    public ProjetoAreaServicoController(IProjetoAreaServicoAppService projetoAreaServicoAppService, IColaboradorAppService colaboradorAppService, IPerfilAppService perfilAppService)
    {
      _projetoAreaServicoAppService = projetoAreaServicoAppService;
      _colaboradorAppService = colaboradorAppService;
      _perfilAppService = perfilAppService;
    }

    // GET: ProjetoAreaServicoViewModels
    public IActionResult Index()
    {
      RelatorioProjetosViewModel relatorio = new RelatorioProjetosViewModel();
      relatorio.ListaProjetos = new List<ProjetoAreaServicoViewModel>();

      var _listaProjetos = _projetoAreaServicoAppService.GetAll();

      foreach (var item in _listaProjetos)
      {
        relatorio.ListaProjetos.Add(new ProjetoAreaServicoViewModel
        {
          Id = item.Id,
          ColaboradorId = item.ColaboradorId,
          Descricao = item.Descricao,
          Nome = item.Nome,
          PerfilId = item.PerfilId,
          Colaborador = item.Colaborador,
          Perfil = item.Perfil
        });
      }

      ViewData["ColaboradorId"] = new SelectList(_colaboradorAppService.GetAll(), "Id", "Nome");
      // ViewData["ColaboradorId"].Add(0, "todos", "id", "Nome");
      ViewData["PerfilId"] = new SelectList(_perfilAppService.GetAll(), "Id", "Nome");

      return View(relatorio);
    }
    public ActionResult Filter(RelatorioProjetosViewModel model)
    {
      if (ModelState["PerfilId"].RawValue.Equals("Todos"))
        ModelState.Remove("PerfilId");
      if (ModelState["ColaboradorId"].RawValue.Equals("Todos"))
        ModelState.Remove("ColaboradorId");
      if (model.DataInicio > model.DataFim)
        ModelState.AddModelError("DataInicio", "A data inicial deve ser menor do que a data final");

      RelatorioProjetosViewModel relatorio = new RelatorioProjetosViewModel();
      relatorio.ListaProjetos = new List<ProjetoAreaServicoViewModel>();

      var _listaProjetos = _projetoAreaServicoAppService.GetAll();

      foreach (var item in _listaProjetos)
      {
        relatorio.ListaProjetos.Add(new ProjetoAreaServicoViewModel
        {
          Id = item.Id,
          ColaboradorId = item.ColaboradorId,
          Descricao = item.Descricao,
          Nome = item.Nome,
          PerfilId = item.PerfilId,
          Colaborador = item.Colaborador,
          Perfil = item.Perfil
        });
      }

      ViewData["ColaboradorId"] = new SelectList(_colaboradorAppService.GetAll(), "Id", "Nome");
      ViewData["PerfilId"] = new SelectList(_perfilAppService.GetAll(), "Id", "Nome");

      return View("Index", relatorio);
    }

    // GET: ProjetoAreaServicoViewModels/Details/5
    public IActionResult Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var projetoAreaServicoViewModel = _projetoAreaServicoAppService.GetProjetoById(id.Value);
      if (projetoAreaServicoViewModel == null)
      {
        return NotFound();
      }

      return View(projetoAreaServicoViewModel);
    }

    // GET: ProjetoAreaServicoViewModels/Create
    public IActionResult Create()
    {
      ViewData["ColaboradorId"] = new SelectList(_colaboradorAppService.GetAll(), "Id", "Nome");
      var testee = _perfilAppService.GetAll();
      ViewData["PerfilId"] = new SelectList(_perfilAppService.GetAll(), "Id", "Nome");


      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProjetoAreaServicoViewModel projetoAreaServicoViewModel)
    {
      if (!ModelState.IsValid) return View(projetoAreaServicoViewModel);

      _projetoAreaServicoAppService.Register(projetoAreaServicoViewModel);



      return View(projetoAreaServicoViewModel);
    }

    // GET: ProjetoAreaServicoViewModels/Edit/5
    public IActionResult Edit(Guid id)
    {
      if (id == null)
      {
        return NotFound();
      }

      ViewData["ColaboradorId"] = new SelectList(_colaboradorAppService.GetAll(), "Id", "Nome");
      ViewData["PerfilId"] = new SelectList(_perfilAppService.GetAll(), "Id", "Nome");

      var teste = _projetoAreaServicoAppService.GetProjetoColaboradorPerfil(id);
      var projetoAreaServicoViewModel = _projetoAreaServicoAppService.GetProjetoColaboradorPerfil(id);

      if (projetoAreaServicoViewModel == null)
      {
        return NotFound();
      }

      return View(projetoAreaServicoViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProjetoAreaServicoViewModel projetoAreaServicoViewModel)
    {
      if (!ModelState.IsValid) return View(projetoAreaServicoViewModel);

      _projetoAreaServicoAppService.Update(projetoAreaServicoViewModel);

      // validar se a operacao ocorreu com sucesso

      return View(projetoAreaServicoViewModel);
    }

    // GET: ProjetoAreaServicoViewModels/Delete/5
    public IActionResult Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var projetoAreaServicoViewModel = _projetoAreaServicoAppService.GetProjetoById(id.Value);

      if (projetoAreaServicoViewModel == null)
      {
        return NotFound();
      }

      return View(projetoAreaServicoViewModel);
    }

    // POST: ProjetoAreaServicoViewModels/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(Guid id)
    {
      _projetoAreaServicoAppService.Remove(id);

      //return RedirectToAction("Index");
      return RedirectToAction(nameof(Index));
    }
  }
}
