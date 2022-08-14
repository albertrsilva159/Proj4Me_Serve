using Proj4Me.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Proj4Me.Domain.ProjetosAreaServicos.Repository
{
  public interface IProjetoAreaServicoRepository : IRepository<ProjetoAreaServico>
  {
    IEnumerable<ProjetoAreaServico> ObterProjetoPorColaborador(Guid colaboradorId);
    ProjetoAreaServico GetbyIdProjetoColaboradorPerfil(Guid id);
    //Cliente ObterClientePorId(Guid id);
    //void AdicionarCliente(Cliente cliente);
    //void AtualizarCliente(Cliente cliente);
    //ProjetoAreaServico ObterMeuProjetoPorId(Guid id, Guid colaboradorId);
    //IEnumerable<Colaborador> ObterCategorias();
  }
}