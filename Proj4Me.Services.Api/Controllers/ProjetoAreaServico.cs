using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using System.Collections.Generic;
using System;
using Proj4Me.Application.Interfaces;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Application.ViewModels;
using System.Linq;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;

namespace Proj4Me.Services.Api.Controllers
{
  public class ProjetoAreaServico : BaseController
  {
    private readonly IProjetoAreaServicoAppService _projetoAppService;
    private readonly IBus _bus;
    private readonly IProjetoAreaServicoRepository _projetoAreaServicoRepository;
    private readonly IMapper _mapper;

    public ProjetoAreaServico(IDomainNotificationHandler<DomainNotification> notifications,
                             IUser user,
                             IBus bus, IProjetoAreaServicoAppService projetoAppService,
                             IProjetoAreaServicoRepository projetoAreaServicoRepository,
                             IMapper mapper) : base(notifications, user, bus)
    {
      _projetoAppService = projetoAppService;
      _projetoAreaServicoRepository = projetoAreaServicoRepository;
      _mapper = mapper;
      _bus = bus;
    }

    [HttpGet]
    [Route("projetos")]
    //[Authorize(Policy = "Projetos:Gravar")]
    public IEnumerable<ProjetoAreaServicoViewModel> Get()
    { 
      return _mapper.Map<IEnumerable<ProjetoAreaServicoViewModel>>(_projetoAreaServicoRepository.GetAll());
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("projetos/{id:guid}")]
    public ProjetoAreaServicoViewModel Get(Guid id, int version)
    {
      return _mapper.Map<ProjetoAreaServicoViewModel>(_projetoAreaServicoRepository.GetById(id));

      //return _projetoAreaServicoRepository.GetById(id);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("projetos/colaboradores")]
    public IEnumerable<ColaboradorViewModel> ObterColaboradores(Guid idColaborador)
    {
      return _mapper.Map<IEnumerable<ColaboradorViewModel>>(_projetoAreaServicoRepository.ObterProjetoPorColaborador(idColaborador));
    }

    [HttpPost]
    [Route("projetos")]
    [Authorize(Policy = "Gravar")]
    public IActionResult Post([FromBody] ProjetoAreaServicoViewModel projetoAreaServicoViewModel)
    {
      if (!ModelState.IsValid)
      {
        NotificarErroModelInvalida();
        return Response();
      }

      var projetoCommand = _mapper.Map<RegistrarProjetoAreaServicoCommand>(projetoAreaServicoViewModel);

      _bus.SendCommand(projetoCommand);
      return Response(projetoCommand);
    }

    [HttpPut]
    [Route("projetos")]
    [Authorize(Policy = "Gravar")]
    public IActionResult Put([FromBody] ProjetoAreaServicoViewModel projetoAreaServicoViewModel)
    {
      if (!ModelState.IsValid)
      {
        NotificarErroModelInvalida();
        return Response();
      }

      _projetoAppService.Update(projetoAreaServicoViewModel);
      return Response(projetoAreaServicoViewModel);
    }

    [HttpDelete]
    [Route("projetos/{id:guid}")]
    [Authorize(Policy = "Gravar")]
    public IActionResult Delete(Guid id)
    {
      _projetoAppService.Remove(id);
      return Response();
    }
  }
}
