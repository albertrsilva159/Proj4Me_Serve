using AutoMapper;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;
using Proj4Me.Domain.Colaboradores.Commands;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Core.Bus;
using System;
using System.Collections.Generic;


namespace Proj4Me.Application.Services
{
  public class ColaboradorAppService : IColaboradorAppService
  {
    private readonly IBus _bus;
    //private readonly IMapper _mapper;
    private readonly IColaboradorRepository _colaboradorRepository;//repositorio pode sim ser usado na camada de aplication, nao tem problema solicitar informações do banco
    private readonly IMapper _mapper;

    public ColaboradorAppService(IBus bus, IMapper mapper, IColaboradorRepository colaboradorRepository)
    {
      _bus = bus;
      _mapper = mapper;
      _colaboradorRepository = colaboradorRepository;
    }

    public void Register(ColaboradorViewModel eventoViewModel)
    {
      var registroCommand = _mapper.Map<RegistrarColaboradorCommand>(eventoViewModel);
      _bus.SendCommand(registroCommand);
    }
    public void Update(ColaboradorViewModel projetoAreaServicoViewModel)
    {
      var atualizarProjetoAreaServicoCommand = _mapper.Map<AtualizarColaboradorCommand>(projetoAreaServicoViewModel);
      _bus.SendCommand(atualizarProjetoAreaServicoCommand);
    }
    public void Remove(Guid id)
    { _bus.SendCommand(new ExcluirColaboradorCommand(id)); }
    public IEnumerable<ColaboradorViewModel> GetAll()
    { return _mapper.Map<IEnumerable<ColaboradorViewModel>>(_colaboradorRepository.GetAll()); }

    public IEnumerable<ColaboradorViewModel> GetProjetoByColaborador(Guid colaboradorId)
    { return _mapper.Map<IEnumerable<ColaboradorViewModel>>(_colaboradorRepository.ObterColaborador(colaboradorId)); }
    public ColaboradorViewModel GetColaboradorById(Guid id)
    { return _mapper.Map<ColaboradorViewModel>(_colaboradorRepository.GetById(id)); }

    public void Dispose()
    { _colaboradorRepository.Dispose(); }
  }
}
