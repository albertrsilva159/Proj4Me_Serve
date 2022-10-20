using AutoMapper;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Services.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proj4Me.Services.Api.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {     
      CreateMap<Tarefa, TarefaViewModel>();
      CreateMap<ProjetoAreaServico, ProjetoAreaServicoViewModel>();
        //.ForMember(source => source.Tarefas, opt => opt.Ignore());
        //.ForMember(source => source.ListaTarefas, opt => opt.MapFrom(s => s.ListaTarefas));
        //.AfterMap((src, dest) =>
        // {
        //   dest.ListaTarefas = new List<Tarefa>();
        //   foreach (var item in src.ListaTarefas)
        //   {
        //     dest.ListaTarefas.Add(item);
        //   }
        // }

      CreateMap<Colaborador, ColaboradorViewModel>();
      CreateMap<Perfil, PerfilViewModel>();
    }
  }
}