using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Domain.Perfis.Commands;
using Proj4Me.Domain.Perfis.Repository;
using Proj4Me.Services.Api.ViewModels;


namespace Proj4Me.Services.Api.Controllers
{
  public class PerfilController : BaseController
  {
    private readonly IPerfilRepository _perfilRepository;  
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;

    public PerfilController(INotificationHandler<DomainNotification> notifications,
                             IUser user,
                             IPerfilRepository perfilRepository,
                             IMapper mapper,
                             IMediatorHandler mediator) : base(notifications, user, mediator)
    {
  
      _perfilRepository = perfilRepository;
      _mapper = mapper;
      _mediator = mediator;
    }

    [HttpPost]
    [Route("perfis")]
    [AllowAnonymous]
    //[Authorize(Policy = "Projetos")]
    public IActionResult Post([FromBody] PerfilViewModel perfilViewModel)
    {
      if (!ModelState.IsValid)
      {
        NotificarErroModelInvalida();
        return Response();
      }

      var perfilCommand = _mapper.Map<RegistrarPerfilCommand>(perfilViewModel);

      _mediator.EnviarComando(perfilCommand);

      return Response(perfilCommand);
    }
  }
}
