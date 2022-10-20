using Proj4Me.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Proj4Me.Domain.ProjetosAreaServicos.Repository
{
  public interface IProjetoAreaServicoRepository : IRepository<ProjetoAreaServico>
  {
    public IEnumerable<ProjetoAreaServico> ObterProjetoPorColaborador(Guid colaboradorId);
    public ProjetoAreaServico GetbyIdProjetoColaboradorPerfil(Guid id);
    public List<ProjetoAreaServico> ObterTodosNomesProjetos();

    public List<ProjetoAreaServico> Get_All();

    //Cliente ObterClientePorId(Guid id);
    //void AdicionarCliente(Cliente cliente);
    //void AtualizarCliente(Cliente cliente);
    //ProjetoAreaServico ObterMeuProjetoPorId(Guid id, Guid colaboradorId);
    //IEnumerable<Colaborador> ObterCategorias();
  }
}