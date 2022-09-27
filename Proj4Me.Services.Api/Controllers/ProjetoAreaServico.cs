using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using System.Collections.Generic;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;
using Proj4Me.Services.Api.ViewModels;

namespace Proj4Me.Services.Api.Controllers
{
  public class ProjetoAreaServico : BaseController
  {
    private readonly IProjetoAreaServicoRepository _projetoAreaServicoRepository;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public ProjetoAreaServico(INotificationHandler<DomainNotification> notifications,
                             IUser user,                             
                             IProjetoAreaServicoRepository projetoAreaServicoRepository,
                             IMapper mapper,
                             IMediatorHandler mediator) : base(notifications, user, mediator)
    {
      _projetoAreaServicoRepository = projetoAreaServicoRepository;
      _mapper = mapper;
      _mediator = mediator;
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

      _mediator.EnviarComando(projetoCommand);
      
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

      var projetoCommand = _mapper.Map<AtualizarProjetoAreaServicoCommand>(projetoAreaServicoViewModel);

      //_projetoAppService.Update(projetoAreaServicoViewModel);
      _mediator.EnviarComando(projetoCommand);
      return Response(projetoCommand);
    }

    [HttpDelete]
    [Route("projetos/{id:guid}")]
    [Authorize(Policy = "Gravar")]
    public IActionResult Delete(Guid id)
    {
      var projetoAreaServicoViewModel = new ProjetoAreaServicoViewModel { Id = id };
      var projetoCommand = _mapper.Map<ExcluirProjetoAreaServicoCommand>(projetoAreaServicoViewModel);

      _mediator.EnviarComando(projetoCommand);
      return Response();
    }
  }
}
