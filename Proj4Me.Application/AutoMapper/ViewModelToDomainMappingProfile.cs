using AutoMapper;
using Proj4Me.Application.ViewModels;
using Proj4Me.Domain.Colaboradores.Commands;
using Proj4Me.Domain.Perfis.Commands;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;
using System;

namespace Proj4Me.Application.AutoMapper
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public ViewModelToDomainMappingProfile()
    {
      //transforma a classe 
      //CreateMap<ProjetoAreaServicoViewModel, RegistrarProjetoAreaServicoCommand>()
      //    .ConstructUsing(c => new RegistrarProjetoAreaServicoCommand(c.Nome, c.Descricao, c.DescricaoLonga, c.ColaboradorId,
      //         new IncluirClienteProjetoAreaServicoCommand(c.Cliente.Id, c.Cliente.Nome, c.Id)));//id aqui é do projeto
      CreateMap<ProjetoAreaServicoViewModel, RegistrarProjetoAreaServicoCommand>()
    .ConstructUsing(c => new RegistrarProjetoAreaServicoCommand(c.Nome, c.Descricao, c.ColaboradorId, c.PerfilId));//id aqui é do projeto

      //CreateMap<ClienteViewModel, IncluirClienteProjetoAreaServicoCommand>()
      //          .ConstructUsing(c => new IncluirClienteProjetoAreaServicoCommand(Guid.NewGuid(), c.Nome, c.Id));

      CreateMap<ProjetoAreaServicoViewModel, AtualizarProjetoAreaServicoCommand>()
          .ConstructUsing(c => new AtualizarProjetoAreaServicoCommand(c.Id, c.Nome, c.Descricao, c.ColaboradorId, c.PerfilId));

      CreateMap<ProjetoAreaServicoViewModel, ExcluirProjetoAreaServicoCommand>()
          .ConstructUsing(c => new ExcluirProjetoAreaServicoCommand(c.Id));

      ////COLABORADOR

      CreateMap<ColaboradorViewModel, RegistrarColaboradorCommand>()
          .ConstructUsing(c => new RegistrarColaboradorCommand(c.Nome, c.Email));

      CreateMap<ColaboradorViewModel, AtualizarColaboradorCommand>()
        .ConstructUsing(c => new AtualizarColaboradorCommand(c.Id, c.Nome, c.Email));

      CreateMap<ColaboradorViewModel, ExcluirColaboradorCommand>()
          .ConstructUsing(c => new ExcluirColaboradorCommand(c.Id));

      ///Perfil

      CreateMap<PerfilViewModel, RegistrarPerfilCommand>()
    .ConstructUsing(c => new RegistrarPerfilCommand(c.Nome));

      CreateMap<PerfilViewModel, AtualizarPerfilCommand>()
        .ConstructUsing(c => new AtualizarPerfilCommand(c.Id, c.Nome));

      CreateMap<PerfilViewModel, ExcluirPerfilCommand>()
          .ConstructUsing(c => new ExcluirPerfilCommand(c.Id));

    }
  }
}
