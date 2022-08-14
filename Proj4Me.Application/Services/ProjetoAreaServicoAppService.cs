using AutoMapper;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.ViewModels;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Application.Services
{
  public class ProjetoAreaServicoAppService : IProjetoAreaServicoAppService
  {
    private readonly IBus _bus;
    //private readonly IMapper _mapper;
    private readonly IProjetoAreaServicoRepository _eventoRepository;//repositorio pode sim ser usado na camada de aplication, nao tem problema solicitar informações do banco
    private readonly IMapper _mapper;

    public ProjetoAreaServicoAppService(IBus bus, IMapper mapper, IProjetoAreaServicoRepository eventoRepository)
    {
      _bus = bus;
      _mapper = mapper;
      _eventoRepository = eventoRepository;
    }

    public void Register(ProjetoAreaServicoViewModel eventoViewModel)
    {
      var registroCommand = _mapper.Map<RegistrarProjetoAreaServicoCommand>(eventoViewModel);
      _bus.SendCommand(registroCommand);
    }
    public void Update(ProjetoAreaServicoViewModel projetoAreaServicoViewModel)
    {
      var atualizarProjetoAreaServicoCommand = _mapper.Map<AtualizarProjetoAreaServicoCommand>(projetoAreaServicoViewModel);
      _bus.SendCommand(atualizarProjetoAreaServicoCommand);
    }
    public void Remove(Guid id)
    { _bus.SendCommand(new ExcluirProjetoAreaServicoCommand(id)); }
    public IEnumerable<ProjetoAreaServicoViewModel> GetAll()
    { return _mapper.Map<IEnumerable<ProjetoAreaServicoViewModel>>(_eventoRepository.GetAll()); }

    public IEnumerable<ProjetoAreaServicoViewModel> GetProjetoByColaborador(Guid colaboradorId)
    { return _mapper.Map<IEnumerable<ProjetoAreaServicoViewModel>>(_eventoRepository.ObterProjetoPorColaborador(colaboradorId)); }
    public ProjetoAreaServicoViewModel GetProjetoById(Guid id)
    { return _mapper.Map<ProjetoAreaServicoViewModel>(_eventoRepository.GetById(id)); }

    public ProjetoAreaServicoViewModel GetProjetoColaboradorPerfil(Guid id)
    { return _mapper.Map<ProjetoAreaServicoViewModel>(_eventoRepository.GetbyIdProjetoColaboradorPerfil(id)); }

    public void Dispose()
    { _eventoRepository.Dispose(); }


  }
}
