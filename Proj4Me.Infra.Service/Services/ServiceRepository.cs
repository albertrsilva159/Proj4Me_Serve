using Microsoft.Extensions.Configuration;
using Proj4Me.Infra.Service.Interfaces;
using System;

namespace Proj4Me.Infra.Service.Services
{
  public class ServiceRepository : IServiceRepository
  {

    private readonly IConfiguration _configuration;
    public ServiceRepository(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string GerarToken()
    {
      var testeee = _configuration["CredenciaisProj4Me:usuario"];

      return String.Empty;
    }
  }
}
