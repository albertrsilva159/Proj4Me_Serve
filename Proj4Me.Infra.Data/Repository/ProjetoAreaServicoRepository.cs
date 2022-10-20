using Proj4Me.Infra.Data.Context;
using System;
using System.Collections.Generic;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;
using Proj4Me.Infra.Service.Interfaces;
using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Infra.Service.Model;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Proj4Me.Infra.Data.Utils;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient.Server;

namespace Proj4Me.Infra.Data.Repository
{
  public class ProjetoAreaServicoRepository : Repository<ProjetoAreaServico>, IProjetoAreaServicoRepository
  {
    readonly private IServiceRepository _serviceRepository;

    public ProjetoAreaServicoRepository(ProjetoAreaServicoContext context, IServiceRepository serviceRepository) : base(context)
    {
      _serviceRepository = serviceRepository;
    }

    public override IEnumerable<ProjetoAreaServico> GetAll()
    {
      List<TarefaProj4Me> todasTasksPorProjeto = new List<TarefaProj4Me>();
      List<Tarefa> listaTarefas = new List<Tarefa>();
      List<ProjetoAreaServico> lista = new List<ProjetoAreaServico>();



      //Parallel.ForEach(todosProjetosProj4Me, projeto =>
      //{
      foreach (var projeto in _serviceRepository.ListarProjetos()) // Todos os projetos
      {
        var projetoEspecifico = _serviceRepository.ProjetoPorIndex(projeto.Index);// Busca detalhe do processo ( nome cliente)

        todasTasksPorProjeto = _serviceRepository.ListarTasksProjeto(projetoEspecifico.Index); // Busca todas as tasks por projeto

        if (todasTasksPorProjeto.Count > 0)
        {
          listaTarefas = todasTasksPorProjeto.Select(c => new Tarefa
          {
            Index = c.index,
            NomeTarefa = c.title,
 
          }).ToList();
        }

        if (listaTarefas != null && listaTarefas.Count > 0)
        {
          lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(projetoEspecifico.NomeProjeto, projetoEspecifico.Index, projetoEspecifico.Cliente.Nome, listaTarefas));

        }


      }

      //});
      return lista;

      ////var sql3 = "select * " +
      ////"from ProjetoAreaServico proj " +
      ////"inner join colaborador col on col.Id = proj.ColaboradorId " +
      ////"inner join perfil per on per.Id = proj.PerfilId ";
      ////return Db.Database.GetDbConnection().Query<ProjetoAreaServico, Colaborador, Perfil, ProjetoAreaServico>(sql3, (proj, col, per) =>
      ////{
      ////  proj.AtribuirColaborador(col);
      ////  proj.AtribuirPerfil(per);
      ////  return proj;
      ////});
    }

    public List<ProjetoAreaServico> Get_All()
    {
      List<TarefaProj4Me> todasTasksPorProjeto = new List<TarefaProj4Me>();

     
      List<ProjetoProj4Me> todosProjetosProj4Me = _serviceRepository.ListarProjetos();
      //if (Parallel.ForEach(todosProjetosProj4Me, projeto =>
      //{    
       List<ProjetoAreaServico> lista = new List<ProjetoAreaServico>();
      var myInClause = new string[] { "teste albert", "S-Works Melhorias" };
      var listaTodosProjetos = _serviceRepository.ListarProjetos().Where(x => myInClause.Contains(x.NomeProjeto));
      foreach (var projeto in listaTodosProjetos)// Todos os projetos //.Where(x => x.NomeProjeto.Equals("teste albert"))
      {
       
        var projetoEspecifico = _serviceRepository.ProjetoPorIndex(projeto.Index);// Busca detalhe do processo ( nome cliente)
        todasTasksPorProjeto = _serviceRepository.ListarTasksProjeto(projetoEspecifico.Index); // Busca todas as tasks por projeto

        //if (Parallel.ForEach(todasTasksPorProjeto, tarefa =>
        //{
        List<Tarefa> listaTarefas = new List<Tarefa>();
        foreach (var tarefa in todasTasksPorProjeto)
        { 
          
          var todosComentariosTarefa = _serviceRepository.BuscarEsforcoEComentarioTasksProjeto(projetoEspecifico.Index, tarefa.index);
          // tempo total da tarefa
          var somaEsforco = todosComentariosTarefa.Sum(x => Convert.ToInt32(x.effort));
          var tempoTotalTarefaFormatado = TratamentoHorasEsforcoTarefa.ConverteFormataHorasEsforco(somaEsforco);
          // Descricao de todos os comentarios da tarefa
          //var juncaoComentariosTarefa = string.Join(System.Environment.NewLine, todosComentariosTarefa.Select(x => x.effortDate + " - " + x.comment)).Replace("<p>", "").Replace("</p>", "");
          foreach (var detalheTarefa in todosComentariosTarefa)
          {

            listaTarefas.Add(new Tarefa
            {
              Index = tarefa.index,
              NomeTarefa = tarefa?.title != null ? tarefa.title : "Nome tarefa não informado!",
              DataEsforco = detalheTarefa?.effortDate != null ? DateTime.Parse(detalheTarefa.effortDate) : DateTime.MinValue,
              TotalTempoGasto = !tempoTotalTarefaFormatado.Equals(String.Empty) || tempoTotalTarefaFormatado != null ? tempoTotalTarefaFormatado : "Tempo total não informado!",
              TempoGastoDetalhado = detalheTarefa?.effort !=null ? TratamentoHorasEsforcoTarefa.ConverteFormataHorasEsforco(detalheTarefa.effort) : "Tempo não informado!",
              Comentario = detalheTarefa?.comment != null ? detalheTarefa.comment : "Comentário não informado!",
              NomeColaborador = detalheTarefa?.worker?.name != null ? detalheTarefa.worker.name : "Colaborador não informado!"
            });

          }
         

        };
        //).IsCompleted) ;
        lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(projetoEspecifico.NomeProjeto, projetoEspecifico.Index, projetoEspecifico.Cliente.Nome, listaTarefas));
      };

      //).IsCompleted) ;
      return lista;
    }

    public List<ProjetoAreaServico> ObterTodosNomesProjetos()
    {
      var projetos = _serviceRepository.ListarProjetos();
      var projeto = projetos.Select(p => ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(p.NomeProjeto, p.Index, null, null)).ToList(); // pegar index de todos projetos

      List<ProjetoAreaServico> lista = new List<ProjetoAreaServico>();
      Parallel.ForEach(projetos, projeto =>
      {
        lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(projeto.NomeProjeto, projeto.Index, null, null));// buscar 

      });

      return lista;
    }

    public ProjetoAreaServico GetbyIdProjetoColaboradorPerfil(Guid id)
    {
      var sql3 = "select * " +
                  "from ProjetoAreaServico proj " +
                  "inner join colaborador col on col.Id = proj.ColaboradorId " +
                  "inner join perfil per on per.Id = proj.PerfilId " +
                  "where proj.Id = @UID";

      var projeto = Db.Database.GetDbConnection().Query<ProjetoAreaServico, Colaborador, Perfil, ProjetoAreaServico>(sql3, (proj, col, per) =>
      {
        proj.AtribuirColaborador(col);
        proj.AtribuirPerfil(per);

        return proj;
      }, new { UID = id });

      return projeto.FirstOrDefault();
    }

    public IEnumerable<ProjetoAreaServico> ObterProjetoPorPerfil(Guid perfilId)
    {
      return Db.ProjetoAreaServico.Where(p => p.PerfilId.Equals(perfilId));
    }

    public IEnumerable<ProjetoAreaServico> ObterProjetoPorColaborador(Guid colaboradorId)
    {
      return Db.ProjetoAreaServico.Where(p => p.ColaboradorId.Equals(colaboradorId));
    }

    public IEnumerable<ProjetoAreaServico> BuscarPeloFilro(DateTime dataInicio, DateTime dataFim, Guid colaboradorId, Guid perfilId)
    {
      var sql3 = "select * " +
                 "from ProjetoAreaServico proj " +
                 "inner join colaborador col on col.Id = proj.ColaboradorId " +
                 "inner join perfil per on per.Id = proj.PerfilId " +
                 "where proj.Id = @UID";

      return Db.ProjetoAreaServico.Where(p => p.PerfilId.Equals(perfilId));
    }


  }
}
