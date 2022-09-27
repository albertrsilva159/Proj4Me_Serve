using AutoMapper;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;
using Proj4Me.Domain.ProjetosAreaServicos;
using Proj4Me.Services.Api.ViewModels;

namespace Proj4Me.Services.Api.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      CreateMap<ProjetoAreaServico, ProjetoAreaServicoViewModel>();
      // CreateMap<Cliente, ClienteViewModel>();
      CreateMap<Colaborador, ColaboradorViewModel>();
      CreateMap<Perfil, PerfilViewModel>();
    }
  }
}