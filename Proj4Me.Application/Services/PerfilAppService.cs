using AutoMapper;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Perfis.Commands;
using Proj4Me.Domain.Perfis.Repository;
using System;
using System.Collections.Generic;


namespace Proj4Me.Application.Services
{
  public class PerfilAppService : IPerfilAppService
  {
    private readonly IBus _bus;
    //private readonly IMapper _mapper;
    private readonly IPerfilRepository _perfilRepository;//repositorio pode sim ser usado na camada de aplication, nao tem problema solicitar informações do banco
    private readonly IMapper _mapper;

    public PerfilAppService(IBus bus, IMapper mapper, IPerfilRepository perfilRepository)
    {
      _bus = bus;
      _mapper = mapper;
      _perfilRepository = perfilRepository;
    }

    public void Register(PerfilViewModel perfilViewModel)
    {
      var registroCommand = _mapper.Map<RegistrarPerfilCommand>(perfilViewModel);
      _bus.SendCommand(registroCommand);
    }
    public void Update(PerfilViewModel perfilViewModel)
    {
      var atualizarProjetoAreaServicoCommand = _mapper.Map<AtualizarPerfilCommand>(perfilViewModel);
      _bus.SendCommand(atualizarProjetoAreaServicoCommand);
    }
    public void Remove(Guid id)
    { _bus.SendCommand(new ExcluirPerfilCommand(id)); }
    public IEnumerable<PerfilViewModel> GetAll()
    { return _mapper.Map<IEnumerable<PerfilViewModel>>(_perfilRepository.GetAll()); }

    public IEnumerable<PerfilViewModel> GetProjetoByColaborador(Guid perfilId)
    { return _mapper.Map<IEnumerable<PerfilViewModel>>(_perfilRepository.ObterPerfil(perfilId)); }
    public PerfilViewModel GetPerfilById(Guid id)
    { return _mapper.Map<PerfilViewModel>(_perfilRepository.GetById(id)); }

    public void Dispose()
    { _perfilRepository.Dispose(); }
  }
}
