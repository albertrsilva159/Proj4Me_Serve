using System;
using AutoMapper;
using Proj4Me.Infra.CrossCutting.AspNetFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proj4Me.Infra.CrossCutting.Identity.Data;
using Proj4Me.Services.Api.Configurations;
using Proj4Me.Services.Api.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Proj4Me.Services.Api
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      // Contexto do EF para o Identity
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Configura��es de Autentica��o, Autoriza��o e JWT.
      services.AddMvcSecurity(Configuration);

      // Options para configura��es customizadas
      services.AddOptions();

      // MVC com restri��o de XML e adi��o de filtro de a��es.
      services.AddMvc(options =>
      {
        options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
        options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
      });
      
      // Versionamento do WebApi
      services.AddApiVersioning("api/v{version}");

      // AutoMapper
      services.AddAutoMapper(typeof(Startup));

      // Configura��es do Swagger
      services.AddSwaggerConfig();

      // MediatR
      services.AddMediatR(typeof(Startup));

      // Registrar todos os DI
      services.AddDIConfiguration();
    }

    public void Configure(IApplicationBuilder app,
                          IHostingEnvironment env,
                          ILoggerFactory loggerFactory,
                          IHttpContextAccessor accessor)
    {

      #region Logging

      //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      //loggerFactory.AddDebug();


      #endregion

      #region Configura��es MVC

      app.UseCors(c =>
      {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
      });
 app.UseHttpsRedirection();
 app.UseRouting();
      app.UseStaticFiles();
      app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });


      //app.UseMvc();      

      #endregion

      #region Swagger

      if (env.IsProduction())
      {
        // Se n�o tiver um token v�lido no browser n�o funciona.
        // Descomente para ativar a seguran�a.
        // app.UseSwaggerAuthorized();
      }


     
     
  
     
      
      app.UseSwagger();
      app.UseSwaggerUI(s =>
      {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Proj4Me v1");// caminho para chegar no swagger
      });

      #endregion
    }
  }
}
