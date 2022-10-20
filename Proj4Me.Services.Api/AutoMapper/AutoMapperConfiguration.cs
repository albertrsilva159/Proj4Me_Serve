using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Proj4Me.Services.Api.AutoMapper
{
  public partial class AutoMapperConfiguration
  {
    public static MapperConfiguration RegisterMappings()
    {
      return new MapperConfiguration(ps =>
      {
        ps.AddProfile(new DomainToViewModelMappingProfile());//dominio para viewmodel
        ps.AddProfile(new ViewModelToDomainMappingProfile());//viewmodel para comando
        ps.AllowNullCollections = true;
        ps.AllowNullDestinationValues = true;
      });
    }
  }
}
