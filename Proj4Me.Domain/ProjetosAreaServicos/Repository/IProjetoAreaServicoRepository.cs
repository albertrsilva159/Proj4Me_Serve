using Proj4Me.Domain.Clientes;
using Proj4Me.Domain.Colaboradores;
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
    public void AdicionarProjetos(List<ProjetoAreaServico> projetos);
    public void AdicionarColaborador(List<Colaborador> colaboradores);
    public void AdicionarCliente(Cliente cliente);
    //Cliente ObterClientePorId(Guid id);
    //void AdicionarCliente(Cliente cliente);
    //void AtualizarCliente(Cliente cliente);
    //ProjetoAreaServico ObterMeuProjetoPorId(Guid id, Guid colaboradorId);
    //IEnumerable<Colaborador> ObterCategorias();
  }
}