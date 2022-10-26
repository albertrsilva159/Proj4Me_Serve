using Proj4Me.Infra.Data.Context;
using System;
using System.Data;
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
using Proj4Me.Domain.Clientes;

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
          lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(projetoEspecifico.NomeProjeto, projetoEspecifico.Index, listaTarefas));

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
      CriarCarga();

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
              TempoGastoDetalhado = detalheTarefa?.effort != null ? TratamentoHorasEsforcoTarefa.ConverteFormataHorasEsforco(detalheTarefa.effort) : "Tempo não informado!",
              Comentario = detalheTarefa?.comment != null ? detalheTarefa.comment : "Comentário não informado!",
              NomeColaborador = detalheTarefa?.worker?.name != null ? detalheTarefa.worker.name : "Colaborador não informado!"
            });

          }


        };
        //).IsCompleted) ;
        lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(projetoEspecifico.NomeProjeto, projetoEspecifico.Index, listaTarefas));
      };

      //).IsCompleted) ;
      return lista;
    }

    public List<ProjetoAreaServico> ObterTodosNomesProjetos()
    {
      var projetos = _serviceRepository.ListarProjetos();
      var projeto = projetos.Select(p => ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(p.NomeProjeto, p.Index, null)).ToList(); // pegar index de todos projetos

      List<ProjetoAreaServico> lista = new List<ProjetoAreaServico>();
      Parallel.ForEach(projetos, projeto =>
      {
        lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoBasico(projeto.NomeProjeto, projeto.Index, null));// buscar 

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

    public void CriarCarga()
    {
      var tabelaTemporaria = CriarTabelaTemporaria();
      List<ProjetoAreaServico> lista = new List<ProjetoAreaServico>();
      //// lista de projetos com colaborador e cliente
      List<ProjetoAreaServico> TabelaProjeto = new List<ProjetoAreaServico>();
      /// cliente
      Cliente cliente = new Cliente();
      /// lista de colaboradores
      List<Colaborador> listaColaboradores = new List<Colaborador>();
      /// Consultar todos os projetos para poder pegar o index de cada um
      List<ProjetoProj4Me> todosProjetosProj4Me = _serviceRepository.ListarProjetos();
      ///ler todos os projetos e pegar dados especificos de cadas projeto
      foreach (var projeto in todosProjetosProj4Me)// Todos os projetos //.Where(x => x.NomeProjeto.Equals("teste albert"))
      {
        // consulta cada projeto da lista
        var projetoEspecifico = _serviceRepository.ProjetoPorIndex(projeto.Index);
        // criar cliente
        cliente = new Cliente
        {
          IndexClienteProj4Me = Convert.ToInt32(projetoEspecifico?.Client?.IndexClienteProj4Me),
          Nome = projetoEspecifico?.Client?.Nome,
          Id = Guid.NewGuid()
        };
        //incluir colaborador
        foreach (var time in projetoEspecifico?.TeamMembers)
        {
          Colaborador colaborador = new Colaborador();
          colaborador.IndexColaboradorProj4Me = time.Collaborator.IndexColaboradorProj4Me;
          colaborador.Nome = time.Collaborator.Name;
          colaborador.Email = time.Collaborator.BusinessEmail;
          colaborador.Id = Guid.NewGuid();

          listaColaboradores.Add(colaborador);
          // Preenche tabela de projeto
          ///tabelaTemporaria.Rows.Add(projetoEspecifico.Index, projetoEspecifico.NomeProjeto, cliente.Id, cliente.Nome, colaborador.Id, colaborador.Nome);

          lista.Add(ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServico(Convert.ToInt32(projetoEspecifico.Index), projetoEspecifico.NomeProjeto, cliente.Id, colaborador.Id));
          // busca todas as tasks por projeto
          var todasTasksPorProjeto = _serviceRepository.ListarTasksProjeto(projetoEspecifico.Index);
        }

        AdicionarCliente(cliente);
        AdicionarColaborador(listaColaboradores);
        AdicionarProjetos(lista);




        //////////var results = tabelaTemporaria.Rows.Cast<DataRow>()
        //////////       .FirstOrDefault(x => x.Field<string>("NomeCliente") == "NEXT");

      }


    }

    public DataTable CriarTabelaTemporaria()
    {
      var dtbProjeto = new DataTable();

      //Projeto
      dtbProjeto.Columns.Add("IndexProjetoProj4Me", typeof(int));
      dtbProjeto.Columns.Add("NomeProjeto", typeof(string));
      //Cliente
      dtbProjeto.Columns.Add("IndexClienteProj4Me", typeof(int));
      dtbProjeto.Columns.Add("NomeCliente", typeof(string));
      //Colaborador
      dtbProjeto.Columns.Add("IndexColaboradorProj4Me", typeof(int));
      dtbProjeto.Columns.Add("NomeColaborador", typeof(string));


      return dtbProjeto;

    }

    public void AdicionarColaborador(List<Colaborador> colaboradores)
    {
      var lista1 = Db.Colaborador.ToList();
      //var listaNovosColaboradores = colaboradores.Except(Db.Colaborador.ToList());

      //var listaNovosColaboradores = colaboradores.Select(x => x.IndexColaboradorProj4Me).Except(Db.Colaborador.ToList().Select(y => y.IndexColaboradorProj4Me)).ToList();

      //var listaNovosColaboradores = colaboradores.ExceptBy(Db.Colaborador.ToList().Select(obj1 => obj1.IndexColaboradorProj4Me)
      //  obj2 => obj2.IndexColaboradorProj4Me).ToList();

      var listaNovosColaboradores = colaboradores.Where(x => !Db.Colaborador.ToList().Any(y => y.IndexColaboradorProj4Me == x.IndexColaboradorProj4Me)).ToList();

      if (listaNovosColaboradores.Count() > 0)
      {
        foreach (var colaborador in listaNovosColaboradores)
        {
          Db.Colaborador.Add(colaborador);
        }

        Db.SaveChanges();
      }
    }

    public void AdicionarCliente(Cliente cliente)
    {
      //Verifica se cliente já existe na base   
      if (!Db.Cliente.Where(x => x.IndexClienteProj4Me.Equals(cliente.IndexClienteProj4Me)).Any())
      {
        Db.Cliente.Add(cliente);
        Db.SaveChanges();
      }
    }

    public void AdicionarProjetos(List<ProjetoAreaServico> projetos)
    {
      //var listaNovosProjetos = projetos.Except(Db.ProjetoAreaServico.ToList());
      var listaNovosProjetos = projetos.Where(x => !Db.ProjetoAreaServico.ToList().Any(y => y.IndexProjetoProj4Me == x.IndexProjetoProj4Me)).ToList();

      if (listaNovosProjetos.Count() > 0)
      {
        foreach (var projeto in listaNovosProjetos)// Traz somente colaborador que não existe
        {
          Db.ProjetoAreaServico.Add(projeto);
        }

        Db.SaveChanges();
      }
    }
  }
}
