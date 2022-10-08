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
      var teste = _serviceRepository.GerarToken();
      List<ProjetoProj4Me> todosProjetosProj4Me = _serviceRepository.ListarProjetos();

      //foreach (var projeto in todosProjetosProj4Me)
      //{
      //  var projetoEspecifico = _serviceRepository.ListarProjetoEspecifico(projeto.index);

      //}

      

      var retorno = todosProjetosProj4Me.Select(p => ProjetoAreaServico.ProjetoAreaServicoFactory.NovoProjetoAreaServicoCompleto(Guid.NewGuid(), p.title, p.title, null, null));

      var listaVindaProj4Me = retorno;

      return listaVindaProj4Me;

      var sql3 = "select * " +
      "from ProjetoAreaServico proj " +
      "inner join colaborador col on col.Id = proj.ColaboradorId " +
      "inner join perfil per on per.Id = proj.PerfilId ";
      return Db.Database.GetDbConnection().Query<ProjetoAreaServico, Colaborador, Perfil, ProjetoAreaServico>(sql3, (proj, col, per) =>
      {
        proj.AtribuirColaborador(col);
        proj.AtribuirPerfil(per);
        return proj;
      });
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
