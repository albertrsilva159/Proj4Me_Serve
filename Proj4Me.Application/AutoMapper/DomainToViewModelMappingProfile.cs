using AutoMapper;
using Proj4Me.Application.ViewModels;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Perfis;
using Proj4Me.Domain.ProjetosAreaServicos;

namespace Proj4Me.Application.AutoMapper
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